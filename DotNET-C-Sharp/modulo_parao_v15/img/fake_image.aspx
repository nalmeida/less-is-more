<%@ Page Language="VB" %>
<%@ Import Namespace="System.Drawing.Drawing2D" %>
<%@ Import Namespace="System.Drawing" %>
<%@ Import Namespace="System.Drawing.Text" %>
<%@ Import Namespace="System.Drawing.Imaging" %>
<script language="VB" runat="server">

	' migué pra gerar imagens "fake" no desenvolvimento de páginas
	' @author Marcelo Miranda Carneiro - mcarneiro@gmail.com
	' @since 10/07/2008
	' @version 1.0.0
	' @usage
	'	<code>
	'		<img src="../img/fake_Image.aspx?quad=100" alt="teste" title="teste" /> ' gera imagem quadrada com 100px
	'		<img src="../img/fake_Image.aspx?width=120&height=100" alt="teste" title="teste" /> ' gera imagem com 120 x 100px
	'	</code>

	Sub Page_Load(sender As Object, e As EventArgs)

		Dim qQuad = Request.QueryString("quad")
		Dim qWidth = Request.QueryString("width")
		Dim qHeight = Request.QueryString("height")
		
		if qQuad <> "" then
			qWidth = qQuad
			qHeight = qQuad
		else
			if qWidth = "" then
				qWidth = 100
			end if

			if qHeight = "" then
				qHeight = 100
			end if
		end if
		
		Dim letterWidth as Integer = 5.5
		Dim letterHeight as Integer = 9
		Dim fontSize as Integer = 9
		
		
		Dim width as Integer = Convert.ToInt32(qWidth)
		Dim height as Integer = Convert.ToInt32(qHeight)
		
		Dim oBitmap As Bitmap = New Bitmap(width,height)
		
		Dim oGraphic As Graphics = Graphics.FromImage(oBitmap)
		

		Dim sText As String = width & "x" & height
		Dim sFont As String = "Courier"

		Dim oBrush As New SolidBrush(Color.LightGray)
		Dim oBrushWrite As New SolidBrush(Color.Black)

		oGraphic.FillRectangle(oBrush, 0, 0, width, height)
		'oGraphic.TextRenderingHint = TextRenderingHint.AntiAlias
		
		Dim oFont As New Font(sFont, fontSize, FontStyle.Regular, GraphicsUnit.Pixel)
		Dim oPoint As New PointF((qWidth / 2) - ((sText.length * letterWidth) / 2), (qHeight / 2)  - (letterHeight / 2))

		oGraphic.DrawString(sText, oFont, oBrushWrite, oPoint)

		Response.ContentType = "image/jpeg"
		oBitmap.Save (Response.OutputStream, ImageFormat.Jpeg)

		'oBitmap.Save(Server.MapPath("gen_img.jpg"), ImageFormat.Jpeg)
		'Response.Write("View the generated image <a target=""_blank"" href=""gen_img.jpg"">here</a>")

	End Sub
</script>