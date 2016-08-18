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
            If clArgs(1) = "-pain" Then
                servo = "2"
                angle = clArgs(2)
                time = clArgs(3)
            ElseIf clArgs(1) = "-tease" Then
                servo = "1"
                angle = clArgs(2)
                time = clArgs(3)
            ElseIf clArgs(1) = "-release" Then
                Release()
                Exit Sub
            ElseIf clArgs(1) = "-lock" Then
                Release()
                Exit Sub
            ElseIf clArgs(1) = "-?" Then
                Console.WriteLine("Help")
                Console.WriteLine("Valid options")
                Exit Sub

            End If
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
            Exit Sub

        End Try

        mySerialPort.Write(servo + angle)
        System.Threading.Thread.Sleep(time * 1000)
        mySerialPort.Write(servo + "0")

    End Sub
    Sub Release()
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

        mySerialPort.Write("3")
    End Sub
End Module
