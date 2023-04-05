/*
	Ditto is an application used for copying multiple items to the clipboard.
    This script pastes multiple items that have been copied from Ditto's clipboard.
*/

#SingleInstance, Force
#NoEnv  ; Recommended for performance and compatibility with future AutoHotkey releases.
; #Warn  ; Enable warnings to assist with detecting common errors.
SendMode Input  ; Recommended for new scripts due to its superior speed and reliability.
SetWorkingDir %A_ScriptDir%  ; Ensures a consistent starting directory.

; Import variables from other files
InputBoxWidth = 400
InputBoxHeight = 150
FileRead, delayGeneral, helper\delayGeneral.txt
FileRead, hotkeyForPasteMultipleImagesFromDitto, helper\hotkeyForPasteMultipleImagesFromDitto.txt
Hotkey,%hotkeyForPasteMultipleImagesFromDitto%,Button
Return
Button:

; Prompt user for how many images to be pasted
InputBox, NumberOfImagesCopied, pasteImagesAndAudioFromDitto, How many images should be pasted from Ditto?, , InputBoxWidth, InputBoxHeight

; Delays (called by Sleep) are needed because neither the computer nor Ditto operate instantaneously
Sleep, delayGeneral
; Paste all images from Ditto into the text box
Loop, % NumberOfImagesCopied
{
	Run, helper\ActivateDitto.ahk
	Sleep, delayGeneral
	Loop, % NumberOfImagesCopied-1
	{
		SendEvent, {Down}
		Sleep, delayGeneral
	}
	Send, {Enter}
	Sleep, delayGeneral
}

; Go back to the very first image
Loop, % NumberOfImagesCopied
{
	SendEvent, {Left}
	Sleep, delayGeneral
}
; Put each image on a separate line
Loop, % NumberOfImagesCopied
{
	SendEvent, {Right}
	Sleep, delayGeneral
	SendEvent, {Enter}
	Sleep, delayGeneral
}

return