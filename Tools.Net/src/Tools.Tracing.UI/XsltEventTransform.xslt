<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:output method="text" />
    <xsl:template match="/">
        <xsl:apply-templates select="descendant::Message"/>
    </xsl:template>
    <xsl:template match="Message">
        <xsl:value-of select="."/>
    </xsl:template>
</xsl:stylesheet>