
Namespace EmotionalStateMachine
    ''' <summary>
    ''' State used for Emotional State machine
    ''' </summary>
    Public Class IEmotionalState
        ''' <summary>
        ''' State name - USed to identify state
        ''' </summary>
        ''' <returns></returns>
        Property EmotionalStateName As String
    End Class
    ''' <summary>
    ''' State machine Controller - Used to Control and create the Emotional State machine controller
    ''' </summary>
    Public Class EmotionalStateMachineController
        ''' <summary>
        ''' Current state machine
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property CurrentStateMachine As EmotionalStateMachine
            Get
                If StateMachine IsNot Nothing Then
                    Return StateMachine
                Else
                    Return Nothing
                End If
            End Get
        End Property
        Public State As String = ""
        Private WithEvents StateMachine As EmotionalStateMachine
        ''' <summary>
        ''' Creates a new state machine to be controlled by the state machine controller; 
        ''' </summary>
        ''' <param name="InitialState">initial state of the state machine</param>
        ''' <param name="States">list of states held in the state machine rto be created</param>
        Public Sub CreateStateMachine(ByRef InitialState As String, ByRef States As List(Of IEmotionalState))
            StateMachine = New EmotionalStateMachine(InitialState, States)
        End Sub
        Public Shared Function CreateEmotionalStates(ByRef ListOfStates As List(Of String)) As List(Of IEmotionalState)
            Dim Lst As New List(Of IEmotionalState)
            For Each item In ListOfStates
                Dim Emotion As New IEmotionalState
                Emotion.EmotionalStateName = item
            Next
            Return Lst
        End Function
        ''' <summary>
        ''' Returns current state of the state machine
        ''' </summary>
        ''' <returns></returns>
        Public Function GetCurrentState() As IEmotionalState
            If StateMachine IsNot Nothing Then
                Return StateMachine.GetState()
            End If
            Return Nothing
        End Function
        ''' <summary>
        ''' Adds as new state to the state machine
        ''' </summary>
        ''' <param name="State"></param>
        Public Sub AddState(ByRef State As IEmotionalState)
            StateMachine.AddState(State)
        End Sub
        ''' <summary>
        ''' Changes current state of the state machine
        ''' </summary>
        ''' <param name="State"></param>
        Public Sub ChangeState(ByRef State As String)
            StateMachine.ChangeState(State)
        End Sub
        Private Sub StateMachine_StateChanged(ByRef Sender As EmotionalStateMachine, ByRef CurrentState As IEmotionalState) Handles StateMachine.StateChanged
            State = CurrentState.EmotionalStateName
        End Sub
    End Class
    ''' <summary>
    ''' Emotional State machine
    ''' </summary>
    Public Class EmotionalStateMachine
        ''' <summary>
        ''' Informs The Controller that the state as been changed
        ''' </summary>
        ''' <param name="Sender"></param>
        ''' <param name="CurrentState"></param>
        Public Event StateChanged(ByRef Sender As EmotionalStateMachine, ByRef CurrentState As IEmotionalState)
        Private States As New List(Of IEmotionalState)
        Private CurrentState As IEmotionalState
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="State">IEmotionalState.EmotionalStateName used to set the initial state</param>
        ''' <param name="StatesList"></param>
        Public Sub New(ByRef State As String, ByRef StatesList As List(Of IEmotionalState))
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
                    If item.EmotionalStateName = State Then
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
        Public Function GetState() As IEmotionalState
            Return CurrentState
        End Function
        ''' <summary>
        ''' Adds a new state to the state machines held possible states
        ''' </summary>
        ''' <param name="State"></param>
        Public Sub AddState(ByRef State As IEmotionalState)
            If State IsNot Nothing Then
                If State.EmotionalStateName IsNot Nothing Then

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
        Private Function CheckState(ByRef State As IEmotionalState) As Boolean
            Dim found As Boolean = False
            For Each item In States
                If item.EmotionalStateName = State.EmotionalStateName = True Then
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
                If item.EmotionalStateName = State = True Then
                    found = True
                Else

                End If
            Next
            Return found
        End Function
    End Class
End Namespace

