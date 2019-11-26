<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:ms="urn:schemas-microsoft-com:xslt"
>
  <xsl:output method="text" omit-xml-declaration="yes" indent="no"/>
  <xsl:template match="/">
    <xsl:text>Тема документа: </xsl:text>
    <xsl:value-of select="Data/CardDocument/MainInfo/@Name"/>
    <xsl:text>&#10;</xsl:text>
    
    <xsl:text>Дата регистрации: </xsl:text>
    <xsl:value-of select="ms:format-date(Data/CardDocument/MainInfo/@RegDate, 'dd MMM yyyy')"/>
    <xsl:text>&#10;</xsl:text>

    <xsl:text>Содержание: </xsl:text>
    <xsl:value-of select="Data/CardDocument/MainInfo/@Content"/>
    <xsl:text>&#10;</xsl:text>

    <xsl:text>Тип доставки: </xsl:text>
    <xsl:variable name="delivery-type-id" select="Data/CardDocument/MainInfo/@DeliveryTypeId"/>     
    <xsl:value-of select="//ItemsRow[@RowID=$delivery-type-id]/@Name"/>
    <xsl:text>&#10;</xsl:text>

    <xsl:text>Регистрационный номер: </xsl:text>
    <xsl:variable name="reg-number-id" select="Data/CardDocument/MainInfo/@RegNumber"/>
    <xsl:value-of select="//NumbersRow[@RowID=$reg-number-id]/@Number"/>
    <xsl:text>&#10;</xsl:text>

    <xsl:text>Регистратор: </xsl:text>
    <xsl:variable name="author-id" select="Data/CardDocument/MainInfo/@Author"/>
    <xsl:value-of select="concat(//EmployeesRow[@RowID=$author-id]/@LastName, ' ', //EmployeesRow[@RowID=$author-id]/@FirstName)"/>    
    <xsl:text>&#10;</xsl:text>

    <xsl:text>Получатели: </xsl:text>
    <xsl:text>&#10;</xsl:text>
    
    <xsl:for-each select='Data/CardDocument/ReceiversStaff/ReceiversStaffRow'>
      <xsl:text>  - </xsl:text>
      <xsl:variable name="receiver-id" select="@ReceiverStaff"/>
      <xsl:value-of select="concat(//EmployeesRow[@RowID=$receiver-id]/@LastName, ' ', //EmployeesRow[@RowID=$receiver-id]/@FirstName )"/>
      <xsl:text>&#10;</xsl:text>
    </xsl:for-each>    
  </xsl:template>

</xsl:stylesheet>