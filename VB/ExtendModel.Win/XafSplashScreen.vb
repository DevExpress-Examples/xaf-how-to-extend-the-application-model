Imports System
Imports System.Drawing
Imports System.IO
Imports System.Reflection
Imports DevExpress.ExpressApp.Win.Utils
Imports DevExpress.Skins
Imports DevExpress.Utils.Drawing
Imports DevExpress.Utils.Svg
Imports DevExpress.XtraSplashScreen

Namespace ExtendModel.Win
	Partial Public Class XafSplashScreen
		Inherits SplashScreen

		Protected Overrides Sub DrawContent(ByVal graphicsCache As GraphicsCache, ByVal skin As Skin)
			Dim bounds As Rectangle = ClientRectangle
			bounds.Width -= 1
			bounds.Height -= 1
			graphicsCache.Graphics.DrawRectangle(graphicsCache.GetPen(Color.FromArgb(255, 87, 87, 87), 1), bounds)
		End Sub
		Protected Sub UpdateLabelsPosition()
			labelApplicationName.CalcBestSize()
			Dim newLeft As Integer = (Width - labelApplicationName.Width) \ 2
			labelApplicationName.Location = New Point(newLeft, labelApplicationName.Top)
			labelSubtitle.CalcBestSize()
			newLeft = (Width - labelSubtitle.Width) \ 2
			labelSubtitle.Location = New Point(newLeft, labelSubtitle.Top)
		End Sub
		Public Sub New()
			InitializeComponent()
			Me.labelCopyright.Text = "Copyright © " & Date.Now.Year.ToString() & " Company Name" & System.Environment.NewLine & "All Rights Reserved."
			UpdateLabelsPosition()
		End Sub

#Region "Overrides"

		Public Overrides Sub ProcessCommand(ByVal cmd As System.Enum, ByVal arg As Object)
			MyBase.ProcessCommand(cmd, arg)
			If (CType(cmd, UpdateSplashCommand) = UpdateSplashCommand.Description) Then
				labelStatus.Text = CType(arg, String)
			End If
		End Sub

#End Region

		Public Enum SplashScreenCommand
			SomeCommandId
		End Enum
	End Class
End Namespace