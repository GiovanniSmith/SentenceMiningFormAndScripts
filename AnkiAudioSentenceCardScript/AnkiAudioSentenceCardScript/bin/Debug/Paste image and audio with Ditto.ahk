/*
	Ditto is an application used for copying multiple items to the clipboard.
    This script pastes two items that have been copied from Ditto's clipboard (a screenshot and an audio recording).

	If you change anything in this file, don't forget to save the file and reload the script!
*/

#SingleInstance, Force
#NoEnv  ; Recommended for performance and compatibility with future AutoHotkey releases.
; #Warn  ; Enable warnings to assist with detecting common errors.
SendMode Input  ; Recommended for new scripts due to its superior speed and reliability.
SetWorkingDir %A_ScriptDir%  ; Ensures a consistent starting directory.

; The default hotkey that runs this script is Ctrl Shift F2. Feel free to change it in the respective text file.
; ^ is Ctrl, ! is Alt, + is Shift, so ^g is Ctrl G, ^!u is Ctrl Alt U, and ^!+F3 is Ctrl Alt Shift F3
FileRead, hotkeyForPasteImageAndAudioFromDitto, helper\hotkeyForPasteImageAndAudioFromDitto.txt
Hotkey,%hotkeyForPasteImageAndAudioFromDitto%,Button
Return
Button:

; Import variables from other files
FileRead, shortDelay, helper\shortDelay.txt
FileRead, mediumDelay, helper\mediumDelay.txt
FileRead, longDelay, helper\longDelay.txt
FileRead, whenToTakeScreenshotWithShareX, helper\whenToTakeScreenshotWithShareX.txt

Sleep, mediumDelay
if (whenToTakeScreenshotWithShareX = 0) {
    ; if screenshot is taken at beginning (meaning it is copied 1st, and recording is copied 2nd)
    
    Loop, 2 {
        if (A_Index = 2) {
            Send, {Right}
            Sleep, shortDelay
            Send, {Enter}
            Sleep, mediumDelay
        }
        Run, helper\ActivateDitto.ahk
        Sleep, mediumDelay
        SendEvent, {Down}
        Sleep, shortDelay
        Send, {Enter}
        Sleep, mediumDelay
    }
} else if (whenToTakeScreenshotWithShareX = 1) {
    ; if screenshot is taken at end (meaning it is copied 2nd, and recording is copied 1st)

    Loop, 2 {
        Run, helper\ActivateDitto.ahk
        Sleep, mediumDelay
        if (A_Index = 1) {
            Send, {Enter}
            Sleep, mediumDelay
        } else if (A_Index = 2) {
            Send, {Down}
            Sleep, mediumDelay
        }
        Send, {Enter}
    }
} else if (whenToTakeScreenshotWithShareX = 2) {
    ; if not screenshot is taken (meaning only the recording was copied)

    Run, helper\ActivateDitto.ahk
    Sleep, mediumDelay
    Send, {Enter}
}


return