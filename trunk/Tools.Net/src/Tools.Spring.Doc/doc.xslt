<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:s="http://www.springframework.net"
                xmlns:msxsl="urn:schemas-microsoft-com:xslt"
                xmlns:user="urn:my-scripts">

  <msxsl:script language="C#" implements-prefix="user">
    <![CDATA[
     public string getMeta(string source, string meta, string fallback, string group){
        string match = "^.*?\\["+ meta +":(.*?)\\].*$";
       Regex rgx = new Regex(match, RegexOptions.Singleline|RegexOptions.Compiled);
       if (!rgx.IsMatch(source))
          return fallback;
          return rgx.Replace(source, group);
     }
     public string removeMeta(string source){
        string match = "(\\[link:.*)|(\\[sp:.*)|(\\[cm:.*)";
        
       Regex rgx = new Regex(match, RegexOptions.Singleline|RegexOptions.Compiled);
       return rgx.Replace(source, "");
     }
      ]]>
  </msxsl:script>

  <xsl:template xml:space="preserve" match="comment()" mode="inlineobjectcomment">
      <i>
        <xsl:value-of select='user:removeMeta(.)'/>
      </i> 
      <xsl:if test='user:getMeta(., "sp", "", "$1") != ""'>
    <br/>

      <b>Stored procedure:</b>
     <xsl:value-of select='user:getMeta(., "sp", "", "$1")'/>     
        </xsl:if>
      
      <xsl:if test='user:getMeta(., "cm", "", "$1") != ""'>
          <br/>
      <b>CM calls:</b>
     <xsl:value-of select='user:getMeta(., "cm", "", "$1")'/>  
                </xsl:if>

  </xsl:template>
  <xsl:template match="comment()" mode="inlinepropcomment">
    <i>
      - <xsl:value-of select="."/>
    </i>
  </xsl:template>
  <xsl:template match="s:ref">
    <div style="margin-left:60">
      <i>
        <xsl:variable name="cmt" select="preceding-sibling::comment()[1]" />
        <xsl:variable name="obj" select="@object" />
        <xsl:element name="a">
          <xsl:attribute name="href">
            <xsl:value-of select='user:getMeta($cmt, "link", concat("#",$obj), "$1")'/>
          </xsl:attribute>
          <xsl:value-of select="@object"/>
        </xsl:element>
        <div style="margin-left:80">
          <xsl:apply-templates mode="inlineobjectcomment" select="preceding-sibling::comment()[1]"/>
          <br/>
        </div>
      </i>
    </div>
  </xsl:template>
  <xsl:template match="s:entry">
    <div style="margin-left:60">
      <i>
        <xsl:variable name="cmt" select="preceding-sibling::comment()[1]" />
        <xsl:variable name="obj" select="@value-ref" />
        <xsl:element name="a">
          <xsl:attribute name="href">
            <xsl:value-of select='user:getMeta($cmt, "link", concat("#",$obj), "$1")'/>
          </xsl:attribute>
          <xsl:value-of select="@value-ref"/>
        </xsl:element>
        <div style="margin-left:80">
          <xsl:apply-templates mode="inlineobjectcomment" select="preceding-sibling::comment()[1]"/>
          <br/>
        </div>
      </i>
    </div>
  </xsl:template>
  <xsl:template match="/">
    <html>
      <style>
      </style>
      <head>
        <title>Definitions</title>
      </head>
      <body>
        <h1>
          <xsl:apply-templates mode="inlineobjectcomment" select="/comment()[1]"/>
        </h1>

        <xsl:for-each select="s:objects/s:object">
          <xsl:element name="h2">
            <xsl:attribute name="id">
              <xsl:value-of select="@name"/>
            </xsl:attribute>
            <xsl:value-of select="@name"/>
          </xsl:element>
          <xsl:apply-templates mode="inlineobjectcomment" select="preceding-sibling::comment()[1]"/>
          <h3>Configured values</h3>
          <xsl:for-each select="s:constructor-arg">
            <div style="margin-left:40">
              <b>
                <xsl:value-of select="@name"/>
              </b>

              <xsl:apply-templates mode="inlinepropcomment" select="preceding-sibling::comment()[1]"/>
            </div>
            <br/>
            <p>
              <xsl:apply-templates />
            </p>
          </xsl:for-each>
          <xsl:for-each select="s:property">
            <div style="margin-left:40">
              <b>
                <xsl:value-of select="@name"/>
              </b>
              <xsl:apply-templates mode="inlinepropcomment" select="preceding-sibling::comment()[1]"/>
            </div>
          </xsl:for-each>
        </xsl:for-each>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>
