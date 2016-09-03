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
        Dim strcmd As String = String.Empty
        Dim servo As String = String.Empty
        Dim angle As String = String.Empty
        Dim time As String = String.Empty
        Dim intcount As Int16
        If clArgs.Count() > 1 Then
            strcmd = clArgs(1)
            If clArgs.Count() > 2 Then
                angle = clArgs(2)
            End If
            If clArgs.Count() > 3 Then
                time = clArgs(3)
            End If
        End If
        Select Case strcmd
            Case "-pain"
                servo = "2"
            Case "-tease"
                servo = "1"
            Case "-release"
                Release()
                Exit Sub
            Case "-lock"
                Exit Sub
            Case "-?"
                Console.WriteLine("Help")
                Console.WriteLine("Valid options")
                Exit Sub
        End Select



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
