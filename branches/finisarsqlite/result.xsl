<?xml version="1.0" encoding="ISO-8859-1"?>

<xsl:stylesheet version="1.0"
xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

<xsl:template match="/">
<html>
<body>
	<h2>Keyword results</h2>
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
	<h2>Incoming links</h2>
	<p>The number of incoming links is a very significant factor in Google's ranking methods.<br />
	The higher the number of incoming links, the higher your site will (probably) rank</p>
	<xsl:for-each select="resultset/backlinks">
	<p>Number of incoming links to your homepage: <b><xsl:apply-templates /></b></p>
	</xsl:for-each>
</body>
</html>
</xsl:template>

</xsl:stylesheet>
