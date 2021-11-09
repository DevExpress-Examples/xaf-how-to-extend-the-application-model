Imports DevExpress.ExpressApp.Web
Imports DevExpress.ExpressApp.Web.Templates
Imports System

Partial Public Class LoginPage
	Inherits BaseXafPage

	Public Overrides ReadOnly Property InnerContentPlaceHolder() As System.Web.UI.Control
		Get
			Return Content
		End Get
	End Property
End Class
