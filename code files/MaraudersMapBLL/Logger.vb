Imports System.IO
Imports System.IO.File

Public Class Logger
    Function append(ByVal text As String, ByVal type As String) As Boolean
        Dim filename As String = System.AppDomain.CurrentDomain.BaseDirectory & "log.txt"
        '  MsgBox(System.AppDomain.CurrentDomain.BaseDirectory & "log.txt")
        Try
            Dim sw As StreamWriter = AppendText(filename)
            sw.WriteLine(Now() & " : " & type & " : " & text)
            sw.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally


        End Try
        Return True
    End Function
End Class
