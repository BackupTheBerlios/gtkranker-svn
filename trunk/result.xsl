<?xml version="1.0" encoding="ISO-8859-1"?>

<xsl:stylesheet version="1.0"
xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

<xsl:template match="/">
  <html>
  <body>
    <h2>Results</h2>
    <table border="1">
    <tr>
      <th align="left">Keyword</th>
      <th align="left">Position</th>
    </tr>
    <xsl:for-each select="resultset/keywords/keyword">
    <tr>
      <td><xsl:value-of select="@name"/></td>
      <td><xsl:apply-templates /></td>
    </tr>
    </xsl:for-each>
    </table>
    <table>
    <xsl:for-each select="resultset/backlinks">
    <tr>
      <td><<xsl:apply-templates /></td>
    </tr>
    </xsl:for-each>
    </table>
  </body>
  </html>
</xsl:template>

</xsl:stylesheet>