Imports System.Runtime.InteropServices
Imports System.Diagnostics
Imports EnvDTE100
Imports EnvDTE80
Imports EnvDTE
Imports Microsoft.VisualBasic.ControlChars


Module ProcessAttacher

    Sub New()

    End Sub

    Sub Main()
        'HideConsole()

        Dim keepOnLooping As Boolean = True
        Dim cntr As Integer = 0
        'Dim localAll As Process() = Process.GetProcesses()
        Dim processOfInterest() As System.Diagnostics.Process

        While (keepOnLooping)
            cntr += 1
            processOfInterest = System.Diagnostics.Process.GetProcessesByName("ustation")

            If (processOfInterest.Count > 0) Then
                keepOnLooping = False
                startDebugging(processOfInterest(0))
            End If

            'System.Console.WriteLine("Waiting {0}", cntr)
            System.Threading.Thread.Sleep(500)

        End While
    End Sub

    Function startDebugging(proc As System.Diagnostics.Process)
        'Dim dbg5 As Debugger5 = CType(DTE2.Debugger, Debugger5)
        Dim dbg2 As EnvDTE80.Debugger2
        dbg2 = DTE.Debugger
        Dim trans As EnvDTE80.Transport = dbg2.Transports.Item("Default")
        Dim dbgeng(1) As EnvDTE80.Engine
        dbgeng(0) = trans.Engines.Item("Native")
    End Function

    <DllImport("User32.dll", EntryPoint:="FindWindow", ExactSpelling:=False, CharSet:=CharSet.Unicode)> _
    Private Function FindWindow(ByVal lpClassName As String, ByVal lpWindowName As String) As IntPtr
    End Function

    <DllImport("User32.dll", EntryPoint:="ShowWindow", ExactSpelling:=False, CharSet:=CharSet.Unicode)> _
    Private Function ShowWindow(ByVal hWnd As IntPtr, ByVal nCmdShow As Integer) As Boolean
    End Function

    Sub HideConsole()
        Dim hWnd As IntPtr
        hWnd = FindWindow(Nothing, Console.Title)

        If (hWnd <> IntPtr.Zero) Then
            ShowWindow(hWnd, 0)
        End If

    End Sub

End Module
