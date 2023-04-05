/*
	This script outputs a screenshot and an audio recording of any video content to two files, by calling helper in ShareX.

*/

#SingleInstance, Force
#NoEnv  ; Recommended for performance and compatibility with future AutoHotkey releases.
; #Warn  ; Enable warnings to assist with detecting common errors.
SendMode Input  ; Recommended for new scripts due to its superior speed and reliability.
SetWorkingDir %A_ScriptDir%  ; Ensures a consistent starting directory.

; Import variables from other files
FileRead, isRecordingAudio, helper\isRecordingAudio.txt
FileRead, delayGeneral, helper\delayGeneral.txt
FileRead, delayForRecordingToStart, helper\delayForRecordingToStart.txt
FileRead, whenToTakeScreenshotWithShareX, helper\whenToTakeScreenshotWithShareX.txt

FileRead, hotkeyForTakeScreenshotAndRecordAudioWithShareX, helper\hotkeyForTakeScreenshotAndRecordAudioWithShareX.txt
Hotkey,%hotkeyForTakeScreenshotAndRecordAudioWithShareX%,Button
Return
Button:

; If the audio is not recording
if (isRecordingAudio = 0) {
	; 0 = takes screenshot at beginning, 1 = takes screenshot at end. 2 (or any number that's not 0 or 1) = doesn't take any screenshots
	if (whenToTakeScreenshotWithShareX = 0) {
		Run, helper\TakeScreenshotWithShareX.ahk
		Sleep, delayGeneral*2
	}
	
	Run, helper\ToggleRecordAudioWithShareX.ahk
	Sleep, delayForRecordingToStart
	Run, helper\PlayPauseVideo.ahk

	isRecordingAudio = 1
	
	; Otherwise if the audio is recording
} else {
	Run, helper\PlayPauseVideo.ahk
	Sleep, delayGeneral
	Run, helper\ToggleRecordAudioWithShareX.ahk

	if (whenToTakeScreenshotWithShareX = 1) {
		Sleep, delayGeneral*2
		Run, helper\TakeScreenshotWithShareX.ahk
	}

	isRecordingAudio = 0
}

; Update the new state of isRecordingAudio
FileDelete, helper\isRecordingAudio.txt
FileAppend, %isRecordingAudio%, helper\isRecordingAudio.txt

return