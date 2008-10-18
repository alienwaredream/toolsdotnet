/*=================================================================================================
  
$Workfile: Service.cs $

$Revision: 1 $  $Date: 9/13/04 10:14a $

Summary:	The Service structure ...


See Source Code Footer for a complete revision history including revision comments.
Using this code under the EC namespace is subject to negotiate with Navitaire, TMN.
---------------------------------------------------------------------------------------------------
This file is part of the Navitaire dotRez application.
Copyright (C) Navitaire.  All rights reserved.
=================================================================================================*/
using System;

namespace Tools.Remoting.Client.Common
{
#region Structure Service
	/// <summary>
	/// Represents a dotRez remoting service running on a specific host and port.
	/// </summary>
	public struct Service
	{
		/// <summary>
		/// Gets or sets the Service Name.
		/// </summary>
		/// <value>
		/// Name of the service.
		/// </value>
		public string Name;

		/// <summary>
		/// Gets or sets the Service Port.
		/// </summary>
		/// <value>
		/// Port the Service is running on.
		/// </value>
		public uint Port;

		/// <summary>
		/// Gets or sets the Service Host.
		/// </summary>
		/// <value>
		/// Host Name that the Service is running on.
		/// </value>
		public string Host;
	}
#endregion Structure Service
}

#region Footer
/*=================================================================================================
  
$History: Service.cs $
 * 
 * *****************  Version 1  *****************
 * User: Tmcneill     Date: 9/13/04    Time: 10:14a
 * Created in $/Source/Bugatti/PaymentSystem-1B3/PAYMENTSYSTEM-1B3/toppackagesV2/ec/architecture/ClientServices/Common
	/// 
	/// *****************  Version 1  *****************
	/// User: Stan-d       Date: 23/06/04   Time: 15:19
	/// Created in $/toppackages/ec/architecture/ClientServices/Common
	/// 
	/// *****************  Version 1  *****************
	/// User: Stan-d       Date: 1/06/04    Time: 14:36
	/// Created in $/toppackages/Navitaire/dotRez/ClientServices/Common
 * 
 * *****************  Version 3  *****************
 * User: Chrisst      Date: 5/18/04    Time: 8:12a
 * Updated in $/Source/Bugatti/dotRez/ClientServices/ClientServices.Common
 * Changes for new remoting and fix for returned ArrayLists
 * 
 * *****************  Version 2  *****************
 * User: Marct        Date: 1/06/04    Time: 3:36p
 * Updated in $/Source/Bugatti/dotRez/ClientServices/ClientServices.Common
 * Updated Xml doc;
 * 
 * *****************  Version 1  *****************
 * User: Marct        Date: 5/05/03    Time: 11:45a
 * Created in $/Source/Bugatti/dotRez/ClientServices/ClientServices.Common
 * Added EnvironmentXmlDocument class and Service structure.

=================================================================================================*/
#endregion
