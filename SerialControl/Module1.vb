Imports System.IO.Ports

Module Module1
    Private mySerialPort As New SerialPort
    Private clArgs() As String = Environment.GetCommandLineArgs()

    Sub Main()
        Static objMutex As System.Threading.Mutex
        Dim bInitialOwner As Boolean

        objMutex = New System.Threading.Mutex(True, "MyMutex", bInitialOwner)

        If Not bInitialOwner Then
            Console.WriteLine("Application is currently running. Exiting program.")
            Exit Sub 'Or set a flag or something
        End If

        'Parse arguments
        Dim servo As String = String.Empty
        Dim angle As String = String.Empty
        Dim time As String = String.Empty
        If clArgs.Count() > 1 Then
            For i As Integer = 1 To clArgs.Count() Step 2
                If clArgs(i) = "-s" Then
                    servo = clArgs(i + 1)
                ElseIf clArgs(i) = "-a" Then
                    angle = clArgs(i + 1)
                ElseIf clArgs(i) = "-t" Then
                    time = clArgs(i + 1)
                ElseIf clArgs(i) = "-r" Then

                End If
            Next
        End If



        Dim mybaud As String = MySettings.Default.BaudRate
        With mySerialPort
            .PortName = MySettings.Default.ComPort
            .BaudRate = Convert.ToInt32(mybaud)
        End With

        Try
            mySerialPort.Open()
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
        mySerialPort.Write("2180")
    End Sub

End Module
