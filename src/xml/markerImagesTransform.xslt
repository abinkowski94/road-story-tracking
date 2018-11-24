<?xml version="1.0" encoding="UTF-8"?>

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:template match="/" xmlns:mi="http://localhost/cityStoryTracking/markedImage"
        xmlns:im="http://localhost/cityStoryTracking/images"
        xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
        xsi:schemaLocation="http://localhost/cityStoryTracking/markedImage.xsd">
        
        <im:images>
            <image>
                <id><xsl:value-of select="mi:MarkedImage/id" /></id>
                <createDate><xsl:value-of select="mi:MarkedImage/createDate" /></createDate>
                <modificationDate><xsl:value-of select="mi:MarkedImage/modificationDate" /></modificationDate>
                <markerId><xsl:value-of select="mi:MarkedImage/markerId" /></markerId>
                <originalImage><xsl:value-of select="mi:MarkedImage/image" /></originalImage>
                <imageSize>S</imageSize>
                <resizedImage>null</resizedImage>
            </image>
            <image>
                <id><xsl:value-of select="mi:MarkedImage/id" /></id>
                <createDate><xsl:value-of select="mi:MarkedImage/createDate" /></createDate>
                <modificationDate><xsl:value-of select="mi:MarkedImage/modificationDate" /></modificationDate>
                <markerId><xsl:value-of select="mi:MarkedImage/markerId" /></markerId>
                <originalImage><xsl:value-of select="mi:MarkedImage/image" /></originalImage>
                <imageSize>M</imageSize>
                <resizedImage>null</resizedImage>
            </image>
            <image>
                <id><xsl:value-of select="mi:MarkedImage/id" /></id>
                <createDate><xsl:value-of select="mi:MarkedImage/createDate" /></createDate>
                <modificationDate><xsl:value-of select="mi:MarkedImage/modificationDate" /></modificationDate>
                <markerId><xsl:value-of select="mi:MarkedImage/markerId" /></markerId>
                <originalImage><xsl:value-of select="mi:MarkedImage/image" /></originalImage>
                <imageSize>L</imageSize>
                <resizedImage>null</resizedImage>
            </image>
        </im:images>
    </xsl:template>
</xsl:stylesheet>