/*
	This script outputs a screenshot and an audio recording of any video content to two files, by calling helper in ShareX.

	If you change anything in this file, don't forget to save the file and reload the script!
*/

#SingleInstance, Force
#NoEnv  ; Recommended for performance and compatibility with future AutoHotkey releases.
; #Warn  ; Enable warnings to assist with detecting common errors.
SendMode Input  ; Recommended for new scripts due to its superior speed and reliability.
SetWorkingDir %A_ScriptDir%  ; Ensures a consistent starting directory.

; The default hotkey that runs this script is Ctrl Shift F1. Feel free to change it in the respective text file.
; ^ is Ctrl, ! is Alt, + is Shift, so ^g is Ctrl G, ^!u is Ctrl Alt U, and ^!+F3 is Ctrl Alt Shift F3
FileRead, hotkeyForTakeScreenshotAndRecordAudioWithShareX, helper\hotkeyForTakeScreenshotAndRecordAudioWithShareX.txt
Hotkey,%hotkeyForTakeScreenshotAndRecordAudioWithShareX%,Button
Return
Button:
; Import variables from other files
FileRead, isRecordingAudio, helper\isRecordingAudio.txt
FileRead, shortDelay, helper\shortDelay.txt
FileRead, mediumDelay, helper\mediumDelay.txt
FileRead, longDelay, helper\longDelay.txt
FileRead, whenToTakeScreenshotWithShareX, helper\whenToTakeScreenshotWithShareX.txt

; If the audio is not recording
if (isRecordingAudio = 0) {
	; 0 = takes screenshot at beginning, 1 = takes screenshot at end. 2 (or any number that's not 0 or 1) = doesn't take any screenshots
	if (whenToTakeScreenshotWithShareX = 0) {
		Run, helper\TakeScreenshotWithShareX.ahk
	}
	; Delays (called by Sleep) are needed because neither the computer nor ShareX operate instantaneously
	; If this script does not work correctly, try increasing the delays. Ex. Sleep, 750 or Sleep, 1000
	Sleep, longDelay
	Run, helper\ToggleRecordAudioWithShareX.ahk
	Sleep, mediumDelay
	Run, helper\PlayPauseVideo.ahk

	isRecordingAudio = 1
	
	; Otherwise if the audio is recording
} else {
	Run, helper\PlayPauseVideo.ahk
	Run, helper\ToggleRecordAudioWithShareX.ahk

	if (whenToTakeScreenshotWithShareX = 1) {
		Sleep, longDelay
		Run, helper\TakeScreenshotWithShareX.ahk
	}

	isRecordingAudio = 0
}

; Update the new state of isRecordingAudio
FileDelete, helper\isRecordingAudio.txt
FileAppend, %isRecordingAudio%, helper\isRecordingAudio.txt

return