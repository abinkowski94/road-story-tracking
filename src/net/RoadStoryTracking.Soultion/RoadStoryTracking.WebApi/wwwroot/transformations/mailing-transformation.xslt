<?xml version='1.0' ?>
<xsl:stylesheet xmlns:xsl='http://www.w3.org/1999/XSL/Transform' version='1.0'>
  <xsl:template match='/EmailMessage'>
    <h2>
      <xsl:value-of select='UserName' />!
    </h2>
    <p>
      <xsl:value-of select='MainMessage' />
      <br>
        <xsl:value-of select='CallbackUrl' />
      </br>
    </p>
  </xsl:template>
</xsl:stylesheet>