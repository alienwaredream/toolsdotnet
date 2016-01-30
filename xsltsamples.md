Documenting ICE config file
```
<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:template match="/">
    <html>
      <style>

      </style>
      <head>
        <title>Jobs List</title>
      </head>
      <body>
        <h1> Jobs </h1>
        <xsl:for-each select="DaCo/JobList/Job">
          <p>
            <h2>
              <xsl:value-of select="@name"/> Job
            </h2>
            <h3>Tasks</h3>
            <xsl:for-each select="Task">
              <div style="margin-left:40">
                <b>
                  <xsl:value-of select="@name"/>
                </b>
                <br/>Enabled: <xsl:value-of select="@enable"/>
              </div>
              <xsl:variable name="taskname" select="@name"/>
              <div style="margin-left:70">
                <b>
                  dll: <xsl:value-of select="/DaCo/TaskDefinitions/Task[@name=$taskname]/Task_DllName"/>
                </b>
                <br/>
                <xsl:variable name="tasksql" select="/DaCo/TaskDefinitions/Task[@name=$taskname]/SqlFile"/>
                sql:
                <xsl:element name="a">
                  <xsl:attribute name="href">
                    file://<xsl:value-of select="$tasksql"/>
                  </xsl:attribute>
                  <xsl:value-of select="$tasksql"/>
                </xsl:element>
                <br/>
                fact table: <xsl:value-of select="/DaCo/TaskDefinitions/Task[@name=$taskname]/FactTable"/>
                <br/>
                input: <xsl:value-of select="/DaCo/TaskDefinitions/Task[@name=$taskname]/InputDirectories"/>
              </div>
            </xsl:for-each>
          </p>
        </xsl:for-each>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>


```