Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports DevExpress.XtraPrinting
Imports System.IO

Namespace WebApplication1
	Partial Public Class _Default
		Inherits System.Web.UI.Page
		Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)

		End Sub

		Protected Sub ASPxButton1_Click(ByVal sender As Object, ByVal e As EventArgs)
			Dim link As New PrintableComponentLink(New PrintingSystem())
			link.Component = ASPxGridViewExporter1
			Using ms As New MemoryStream()
				link.CreateDocument(False)
				Dim brickEnum As BrickEnumerator = link.PrintingSystem.Document.Pages(0).GetEnumerator()
				Dim maxWidth As Single=0
				Do While brickEnum.MoveNext()
					maxWidth = If(maxWidth > brickEnum.CurrentBrick.Rect.X + brickEnum.CurrentBrick.Rect.Width, maxWidth, brickEnum.CurrentBrick.Rect.X + brickEnum.CurrentBrick.Rect.Width)
				Loop
				Dim freeSpace As Single = (link.PrintingSystem.Document.Pages(0)).PageSize.Width - maxWidth
				If freeSpace > 600 Then
					link.Margins.Left = CInt(Fix(freeSpace / 6))
					link.CreateDocument(False)
				End If
				link.ExportToPdf(ms)
				WriteDataToResponse(ms.ToArray(), ExportType.PDF)
			End Using

		End Sub
		  Public Shared Sub WriteDataToResponse(ByVal data() As Byte, ByVal type As ExportType)
			If data IsNot Nothing AndAlso data.Length > 0 AndAlso type <> ExportType.none Then
				Dim fileEnding As String = String.Empty
				Dim fileContent As String = String.Empty
				Select Case type
					Case ExportType.XLS
						fileContent = "application/ms-excel"
						fileEnding = "xls"
					Case ExportType.PDF
						fileContent = "application/pdf"
						fileEnding = "pdf"
					Case ExportType.CSV
						fileContent = "text/plain"
						fileEnding = "csv"
					Case ExportType.RTF
						fileContent = "text/enriched"
						fileEnding = "rtf"
				End Select
				HttpContext.Current.Response.Clear()
				HttpContext.Current.Response.Buffer = False
				HttpContext.Current.Response.ClearHeaders()
				HttpContext.Current.Response.AppendHeader("Content-Type", fileContent)
				HttpContext.Current.Response.AppendHeader("Content-Transfer-Encoding", "binary")
				HttpContext.Current.Response.AppendHeader("Content-Disposition", "inline; filename=Export." & fileEnding)
				HttpContext.Current.Response.BinaryWrite(data)
				HttpContext.Current.Response.End()
			End If
		  End Sub
	End Class

 Public Enum ExportType
		none = 0
		XLS = 1
		PDF = 2
		CSV = 4
		RTF = 8
 End Enum

End Namespace
