/*
	Ditto is an application used for copying multiple items to the clipboard.
    This script pastes multiple items that have been copied from Ditto's clipboard.

	If you change anything in this file, don't forget to save the file and reload the script!
*/

#SingleInstance, Force
#NoEnv  ; Recommended for performance and compatibility with future AutoHotkey releases.
; #Warn  ; Enable warnings to assist with detecting common errors.
SendMode Input  ; Recommended for new scripts due to its superior speed and reliability.
SetWorkingDir %A_ScriptDir%  ; Ensures a consistent starting directory.

; The default hotkey that runs this script is Ctrl Shift F3. Feel free to change it in the respective text file.
; ^ is Ctrl, ! is Alt, + is Shift, so ^g is Ctrl G, ^!u is Ctrl Alt U, and ^!+F3 is Ctrl Alt Shift F3
FileRead, hotkeyForPasteMultipleImagesFromDitto, helper\hotkeyForPasteMultipleImagesFromDitto.txt
Hotkey,%hotkeyForPasteMultipleImagesFromDitto%,Button
Return
Button:
; Import variables from other files
FileRead, shortDelay, helper\shortDelay.txt
FileRead, mediumDelay, helper\mediumDelay.txt
FileRead, longDelay, helper\longDelay.txt
InputBoxWidth = 400
InputBoxHeight = 150
; Prompt user for how many images to be pasted
InputBox, NumberOfImagesCopied, pasteImagesAndAudioFromDitto, How many images should be pasted from Ditto?, , InputBoxWidth, InputBoxHeight

; Delays (called by Sleep) are needed because neither the computer nor Ditto operate instantaneously
; If this script does not work correctly, try increasing the delays. Ex. Sleep, 750 or Sleep, 1000
Sleep, mediumDelay
; Paste all images from Ditto into the text box
Loop, % NumberOfImagesCopied
{
	Run, helper\ActivateDitto.ahk
	Sleep, mediumDelay
	Loop, % NumberOfImagesCopied-1
	{
		SendEvent, {Down}
		Sleep, shortDelay
	}
	Send, {Enter}
	Sleep, mediumDelay
}

; Go back to the very first image
Loop, % NumberOfImagesCopied
{
	SendEvent, {Left}
	Sleep, mediumDelay
}
; Put each image on a separate line
Loop, % NumberOfImagesCopied
{
	SendEvent, {Right}
	Sleep, mediumDelay
	SendEvent, {Enter}
	Sleep, mediumDelay
}

return