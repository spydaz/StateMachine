
''' <summary>
''' State Machine
''' </summary>
Public Class StateMachine
    Private mMachineName As String = ""
    Public Property MachineName As String
        Get
            Return mMachineName
        End Get
        Set(value As String)
            mMachineName = value
        End Set
    End Property
    ''' <summary>
    ''' Informs The Controller that the state as been changed
    ''' </summary>
    ''' <param name="Sender"></param>
    ''' <param name="CurrentState"></param>
    Public Event StateChanged(ByRef Sender As StateMachine, ByRef CurrentState As State)
    ''' <summary>
    ''' Current states held in the Machine
    ''' </summary>
    Private States As New List(Of State)
    ''' <summary>
    ''' Current State of the machine
    ''' </summary>
    Private CurrentState As State
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="State">IEmotionalState.EmotionalStateName used to set the initial state</param>
    ''' <param name="StatesList"></param>
    Public Sub New(ByRef State As String, ByRef StatesList As List(Of State))
        If StatesList IsNot Nothing Then
            States = StatesList
            If State IsNot Nothing Then
                If CheckState(State) = True Then
                    ChangeState(State)
                End If
            End If
        Else
        End If

    End Sub
    ''' <summary>
    ''' Changes the state to the specified State, which must exist in the state machine
    ''' </summary>
    ''' <param name="State">IEmotionalState.EmotionalStateName</param>
    Public Sub ChangeState(ByRef State As String)
        If State IsNot Nothing Then
            For Each item In States
                If item.StateName = State Then
                    CurrentState = item
                    RaiseEvent StateChanged(Me, item)
                Else
                End If
            Next
        Else
        End If
    End Sub
    ''' <summary>
    ''' Returns  the current state
    ''' </summary>
    ''' <returns></returns>
    Public Function GetState() As State
        Return CurrentState
    End Function
    ''' <summary>
    ''' Adds a new state to the state machines held possible states
    ''' </summary>
    ''' <param name="State"></param>
    Public Sub AddState(ByRef State As State)
        If State IsNot Nothing Then
            If State.StateName IsNot Nothing Then

                If CheckState(State) = False Then
                    States.Add(State)
                Else
                End If
            Else
            End If
        Else
        End If
    End Sub
    ''' <summary>
    ''' checks if the state exists in the the list of states 
    ''' </summary>
    ''' <param name="State">State To be checked </param>
    ''' <returns></returns>
    Private Function CheckState(ByRef State As State) As Boolean
        Dim found As Boolean = False
        For Each item In States
            If item.StateName = State.StateName = True Then
                found = True
            Else

            End If
        Next
        Return found
    End Function
    ''' <summary>
    ''' checks if the state exists in the the list of states
    ''' </summary>
    ''' <param name="State">IEmotionalState.EmotionalStateName</param>
    ''' <returns></returns>
    Private Function CheckState(ByRef State As String) As Boolean
        Dim found As Boolean = False
        For Each item In States
            If item.StateName = State = True Then
                found = True
            Else

            End If
        Next
        Return found
    End Function
End Class
''' <summary>
''' Can be inherited or Used as is
''' </summary>
Public Class State
    ''' <summary>
    ''' Name of State (also used as Identity)
    ''' </summary>
    ''' <returns></returns>
    Public Property StateName As String = ""
End Class
''' <summary>
''' Controller for the state machine, Adds/Removes states / Changes States
''' </summary>
Public Class StateMachineController
    ''' <summary>
    ''' State Machine State Changed
    ''' </summary>
    ''' <param name="Sender"></param>
    ''' <param name="Changed"></param>
    Public Event MachineStateChanged(ByRef Sender As StateMachineController, ByRef Changed As Boolean)
    ''' <summary>
    ''' Current state machine
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property CurrentStateMachine As StateMachine
        Get
            If StateMachine IsNot Nothing Then
                Return StateMachine
            Else
                Return Nothing
            End If
        End Get
    End Property
    Public MachineState As String = ""
    ''' <summary>
    ''' State machine
    ''' </summary>
    Private WithEvents StateMachine As StateMachine
    ''' <summary>
    ''' Creates a new state machine to be controlled by the state machine controller; 
    ''' </summary>
    ''' <param name="InitialState">initial state of the state machine</param>
    ''' <param name="States">list of states held in the state machine rto be created</param>
    Public Sub CreateStateMachine(ByRef InitialState As String, ByRef States As List(Of State))
        StateMachine = New StateMachine(InitialState, States)
    End Sub
    ''' <summary>
    ''' Creates a List of states for the state machine given a list of StateNames 
    ''' Req. for creating a state machine
    ''' </summary>
    ''' <param name="ListOfStates"></param>
    ''' <returns></returns>
    Public Shared Function CreateStates(ByRef ListOfStates As List(Of String)) As List(Of State)
        Dim Lst As New List(Of State)
        For Each item In ListOfStates
            Dim NewState As New State
            NewState.StateName = item
        Next
        Return Lst
    End Function
    ''' <summary>
    ''' Returns current state of the state machine
    ''' </summary>
    ''' <returns></returns>
    Public Function GetCurrentState() As State
        If StateMachine IsNot Nothing Then
            Return StateMachine.GetState()
        End If
        Return Nothing
    End Function
    ''' <summary>
    ''' Adds as new state to the state machine
    ''' </summary>
    ''' <param name="State"></param>
    Public Sub AddState(ByRef State As State)
        StateMachine.AddState(State)
    End Sub
    ''' <summary>
    ''' Changes current state of the state machine
    ''' </summary>
    ''' <param name="State"></param>
    Public Sub ChangeState(ByRef State As String)
        StateMachine.ChangeState(State)
    End Sub
    Private Sub StateMachine_StateChanged(ByRef Sender As StateMachine, ByRef CurrentState As State) Handles StateMachine.StateChanged
        MachineState = CurrentState.StateName
        RaiseEvent MachineStateChanged(Me, True)
    End Sub
End Class