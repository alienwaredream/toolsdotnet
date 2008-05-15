/*
<TokenVerificationResult xmlns="http://schemas.datacontract.org/2004/07/Tools.Common.Authorisation" xmlns:i="http://www.w3.org/2001/XMLSchema-instance">
  <Code>1</Code> 
  <Message>Success</Message> 
  </TokenVerificationResult>
*/
var authorisationServiceUrl = "http://stand-1300/Tools.Web.Host/TokenAuthorisationService.svc/VerifyToken?tokenTarget=fhfhf&token=123456";

application.onAppStart = function()
{
	try
	{
		trace('Vintavu application started');
   	}

	catch(e)
	{
		trace("Exception during application start: " + e);
	}
}
application.onConnect = function(p_client, p_autoSenseBW, p_clientSecurityKey)
{

	trace("Connection requested from IP (" + p_client.ip + ") .Provided security token (" + p_clientSecurityKey + ")");
	
	//if (p_clientSecurityKey == null) //TODO: (SD) Complete
	
	// Create a new XML object.
	var auth = new XML();
	// Set the ignoreWhite property to true (default value is false).
	auth.ignoreWhite = true;
	
	var authData = new AuthData(auth, this, p_client, p_autoSenseBW, p_clientSecurityKey);

	auth.onLoad = function(success) 
	{
		authData.acceptAfterVerification(success);
	}

	try 
	{
		// Load the XML into the flooring object.
		auth.load(authorisationServiceUrl);
	}
	catch(e)
	{
		var err = new Object();
		err.message = "Generic error. Contact support!";
		trace("Exception while authorisation: " + e);
		this.rejectConnection(p_client, err);
	}
}
/*
Application.prototype.rejectForReason(reason)
{
	
}
*/
XMLNode.prototype.GetElementByLName = function(lname)
{
	var elements = new Array();
	if (this.nodeName == lname)
	{
		return this;
	}

	var numChildren = this.childNodes.length;
	//trace(numChildren + " children identified");
	for (var i=0; i<numChildren; i++)
	{
	
		var subElement = this.childNodes[i];
		//trace("child" + i + " with name: " + subElement.nodeName + " and value: " + subElement.nodeValue);
		// NOTE: No longer digging deeper than one level (big perf boost)
		if (subElement.nodeName == lname)
		{
			return subElement;
		}
	}
	return null;
}

AuthData = function(xml, app, client, autoSenseBW, clientSecurityKey)
{
	
	this._xml = xml;
	this._app = app;
	this._client = client;
	this._autoSenseBW = autoSenseBW;
	this._clientSecurityKey = clientSecurityKey;
}
AuthData.prototype._xml;
AuthData.prototype._app;
AuthData.prototype._client;
AuthData.prototype._autoSenseBW;
AuthData.prototype._clientSecurityKey;


AuthData.prototype.acceptAfterVerification = function(success)
{
	trace("Response from the authorisation service: success (" + success + "): " + this._xml);
	var code = this._xml.firstChild.GetElementByLName("Code").firstChild.nodeValue;
	var message = this._xml.firstChild.GetElementByLName("Message").firstChild.nodeValue;
	
	trace("Parsed as Code: (" + code + ") and Message (" + message + ")");
	
	
	if (code == "1")
	{
		trace("granted access to video folder (" + this._client.uri + "/" + this._clientSecurityKey + ") to client with ip (" + this._client.ip + ")");
		
		this._client.readAccess = this._clientSecurityKey;
		
		this._app.acceptConnection(this._client);

			if (this._autoSenseBW)
			{
				this._app.calculateClientBw(this._client);
			}
			else
			{
				this._client.call("onBWDone");
			}
	}
	else
	{
		trace("Rejected access to video (" + this._client.uri +") to client with ip (" + this._client.ip + ")" +
		 "Reason: Code (" + code + "), Message (" + message + ")");
		var err = new Object();
		err.message = message;
		this._app.rejectConnection(this._client, err);
	}
}


Client.prototype.getStreamLength = function(p_streamName) {
	trace("getStreamLength  for "+p_streamName);
	return Stream.length(p_streamName);
}

Client.prototype.checkBandwidth = function() {
	trace("checkBandwidth");
	application.calculateClientBw(this);
}


application.calculateClientBw = function(p_client)
{

	p_client.payload = new Array();
	for (var i=0; i<1200; i++){
		p_client.payload[i] = Math.random();	//16K approx
	}

	var res = new Object();
	res.latency = 0;
	res.cumLatency = 1;
	res.bwTime = 0;
	res.count = 0;
	res.sent = 0;
	res.client = p_client;
	var stats = p_client.getStats();
	var now = (new Date()).getTime()/1;
	res.pakSent = new Array();
	res.pakRecv = new Array();
	res.beginningValues = {b_down:stats.bytes_out, b_up:stats.bytes_in, time:now};
	res.onResult = function(p_val) {

		var now = (new Date()).getTime()/1;
		this.pakRecv[this.count] = now;
		//trace( "Packet interval = " + (this.pakRecv[this.count] - this.pakSent[this.count])*1  );
		this.count++;
		var timePassed = (now - this.beginningValues.time);

		if (this.count == 1) {
			this.latency = Math.min(timePassed, 800);
			this.latency = Math.max(this.latency, 10);
		}


		//trace("count = " + this.count + ", sent = " + this.sent + ", timePassed = " + timePassed);

		// If we have a hi-speed network with low latency send more to determine
		// better bandwidth numbers, send no more than 6 packets
		if ( this.count == 2 && (timePassed<2000))
		{
			this.pakSent[res.sent++] = now;
			this.cumLatency++;
			this.client.call("onBWCheck", res, this.client.payload);
		}
		else if ( this.sent == this.count )
		{	
			// See if we need to normalize latency
			if ( this.latency >= 100 )
			{ // make sure we detect sattelite and modem correctly
				if (  this.pakRecv[1] - this.pakRecv[0] > 1000 )
				{
					this.latency = 100;
				}
			}

			delete this.client.payload;
			// Got back responses for all the packets compute the bandwidth.
			var stats = this.client.getStats();
			var deltaDown = (stats.bytes_out - this.beginningValues.b_down)*8/1000;
			var deltaTime = ((now - this.beginningValues.time) - (this.latency * this.cumLatency) )/1000;
			if ( deltaTime <= 0 )
				deltaTime = (now - this.beginningValues.time)/1000;
			
			var kbitDown = Math.round(deltaDown/deltaTime);

			trace("onBWDone: kbitDown = " + kbitDown + ", deltaDown= " + deltaDown + ", deltaTime = " + deltaTime + ", latency = " + this.latency + "KBytes " + (stats.bytes_out - this.beginningValues.b_down)/1024) ;

			this.client.call("onBWDone", null, kbitDown,  deltaDown, deltaTime, this.latency );
		}
	}
	res.pakSent[res.sent++] = now;
	p_client.call("onBWCheck", res, "");
	res.pakSent[res.sent++] = now;
	p_client.call("onBWCheck", res, p_client.payload);

}
