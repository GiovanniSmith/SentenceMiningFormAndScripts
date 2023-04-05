/*
	Ditto is an application used for copying multiple items to the clipboard.
    This script pastes two items that have been copied from Ditto's clipboard (a screenshot and an audio recording).
*/

#SingleInstance, Force
#NoEnv  ; Recommended for performance and compatibility with future AutoHotkey releases.
; #Warn  ; Enable warnings to assist with detecting common errors.
SendMode Input  ; Recommended for new scripts due to its superior speed and reliability.
SetWorkingDir %A_ScriptDir%  ; Ensures a consistent starting directory.

; Import variables from other files
FileRead, delayGeneral, helper\delayGeneral.txt
FileRead, whenToTakeScreenshotWithShareX, helper\whenToTakeScreenshotWithShareX.txt
FileRead, hotkeyForPasteImageAndAudioFromDitto, helper\hotkeyForPasteImageAndAudioFromDitto.txt
FileRead, shouldPasteImageBeforeAudio, helper\shouldPasteImageBeforeAudio.txt
PasteImageThenAudio() {
    
}
PasteAudioThenImage() {

}

Hotkey,%hotkeyForPasteImageAndAudioFromDitto%,Button
Return
Button:

Sleep, delayGeneral
if (whenToTakeScreenshotWithShareX = 0) {
    ; if screenshot is taken at beginning (meaning it is copied 1st, and recording is copied 2nd)
    
    ; paste image then audio
    Loop, 2 {
        if (A_Index = 2) {
            Send, {Right}
            Sleep, delayGeneral
            Send, {Enter}
            Sleep, delayGeneral
        }
        Run, helper\ActivateDitto.ahk
        Sleep, delayGeneral
        SendEvent, {Down}
        Sleep, delayGeneral
        Send, {Enter}
        Sleep, delayGeneral
    }
} else if (whenToTakeScreenshotWithShareX = 1) {
    ; if screenshot is taken at end (meaning it is copied 2nd, and recording is copied 1st)

    ; paste image then audio
    Loop, 2 {
        Run, helper\ActivateDitto.ahk
        Sleep, delayGeneral
        if (A_Index = 1) {
            Send, {Enter}
            Sleep, delayGeneral
        } else if (A_Index = 2) {
            Send, {Down}
            Sleep, delayGeneral
        }
        Send, {Enter}
    }
} else if (whenToTakeScreenshotWithShareX = 2) {
    ; if not screenshot is taken (meaning only the recording was copied)

    Run, helper\ActivateDitto.ahk
    Sleep, delayGeneral
    Send, {Enter}
}


return