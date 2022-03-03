Imports System
Imports System.Drawing
Imports System.Drawing.Imaging

Module Program
    Sub Main(args As String())

        Dim imgInputImage As Image = Nothing

        Try
            If args.Length < 1 Then
                GoTo NORMAL_END
            End If

            For Each arg As String In args
                gif2png(arg)
            Next

        Catch ex As Exception
            WriteLine(ex.Message)

        Finally
            If Not IsNothing(imgInputImage) Then
                imgInputImage.Dispose()
            End If

        End Try

NORMAL_END:

    End Sub

    Sub gif2png(ByRef strInputPath As String)

        Dim imgInputImage As Image = Nothing

        Try
            Dim strInputDir As String = System.IO.Path.GetDirectoryName(strInputPath)
            Dim strInputFileNameWithoutExtension As String = System.IO.Path.GetFileNameWithoutExtension(strInputPath)
            Dim strOputputDir As String = System.IO.Path.Combine(strInputDir, strInputFileNameWithoutExtension)
            Dim objOutputDir As System.IO.DirectoryInfo = System.IO.Directory.CreateDirectory(strOputputDir)

            imgInputImage = Image.FromFile(strInputPath)
            Dim fdimInputImage As New FrameDimension(imgInputImage.FrameDimensionsList(0))
            Dim intFrameCount As Integer = imgInputImage.GetFrameCount(fdimInputImage)

            For ii As Integer = 1 To intFrameCount
                imgInputImage.SelectActiveFrame(fdimInputImage, ii - 1)
                Dim strOputputPath As String = System.IO.Path.Combine(strOputputDir, strInputFileNameWithoutExtension + String.Format("{0:00}", ii) & ".png")
                imgInputImage.Save(strOputputPath, System.Drawing.Imaging.ImageFormat.Png)
            Next

        Catch ex As Exception
            WriteLine(ex.Message)

        Finally
            If Not IsNothing(imgInputImage) Then
                imgInputImage.Dispose()
            End If

        End Try
    End Sub

End Module
