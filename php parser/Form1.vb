Imports System.IO
Public Class Form1
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ListBox1.Items.Clear()
        FolderBrowserDialog1.SelectedPath = Nothing
        Dim path As String
        If (FolderBrowserDialog1.ShowDialog() = DialogResult.OK) Then
            path = FolderBrowserDialog1.SelectedPath
            Label2.Text = path
            'Dim files() As String = Directory.GetFiles(path, "*.php", SearchOption.AllDirectories) 'busca solo archivos php en todos los subdirectorios
            Dim files() As String = Directory.GetFiles(path, "*.php") 'busca solo php en el directorio principal para pruebas
            For Each file In files
                ListBox1.Items.Add(file)
            Next
        End If
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        Dim i As Integer = ListBox1.SelectedIndex
        If Not i.Equals(-1) Then
            Dim currentfile As String = ListBox1.Items(i).ToString()
            Label4.Text = currentfile

            If File.Exists(currentfile) Then
                RichTextBox1.Text = ""
                RichTextBox1.Text = File.ReadAllText(currentfile)
                'leer linea por linea y detectar comentarios.
                RichTextBox2.Text = ""
                Dim line As String = ""
                For Each line In File.ReadLines(currentfile)
                    If line.Contains("//") Then
                        Dim start As Integer = line.IndexOf("//")
                        'RichTextBox2.AppendText(line.Substring(start, line.Length - start) + vbCrLf) 'muestra el comentario solamente
                        RichTextBox2.AppendText(line.Substring(0, start) + vbCrLf) 'muestra el contenido sin comentario
                    Else
                        RichTextBox2.AppendText(line + vbCrLf)
                    End If
                Next
            Else
                MsgBox("No se encontro el archivo.")
            End If
        End If
    End Sub
End Class
