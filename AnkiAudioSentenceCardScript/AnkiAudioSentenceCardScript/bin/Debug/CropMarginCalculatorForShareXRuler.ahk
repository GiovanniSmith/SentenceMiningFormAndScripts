/*
	This script requests the copied values from ShareX's ruler tool, and the user's screen resolution.
	It outputs, and copies to the clipboard, four numbers that can be used for ShareX's crop image effect.
*/

#SingleInstance, Force
#NoEnv  ; Recommended for performance and compatibility with future AutoHotkey releases.
; #Warn  ; Enable warnings to assist with detecting common errors.
SendMode Input  ; Recommended for new scripts due to its superior speed and reliability.
SetWorkingDir %A_ScriptDir%  ; Ensures a consistent starting directory.

InputBoxWidth = 400
InputBoxHeight = 150

; https://www.autohotkey.com/board/topic/49606-multiple-input-fields/
; Prompt user for text copied from ShareX's ruler tool, and the user's screen resolution.
Gui, Add, Text, x6 y10 w400 h150 ,
(
	In ShareX, go to Tools > Ruler. Select the desired region, copy the text with Control C (or Command C on Mac), press Escape and paste that text here.

)
Gui, Add, Edit, vRawText x6 y40 w400 h60

Gui, Add, Text, x6 y110 w400 h150 ,
(
	Monitor width
)
Gui, Add, Edit, vMonitorWidth x6 y130 w400 h20, 1920

Gui, Add, Text, x6 y160 w400 h150 ,
(
	Monitor height
)
Gui, Add, Edit, vMonitorHeight x6 y180 w400 h20, 1080

Gui, Add, Button, x160 y210 w80 h60 , Enter

Gui, Add, Text, x6 y280 w400 h240 , Clicking "Enter" will copy new values to your clipboard.

Gui, Show, h300 xCenter
Gui, Show,, calcualteRegionCoordinatesFromShareXRuler

Return

buttonenter:
gui, submit

; Remove extraneous information from text entered
RawText := StrReplace(RawText, " `| ", ",")
RawText := StrReplace(RawText, "X: ", "")
RawText := StrReplace(RawText, "Y: ", "")
RawText := StrReplace(RawText, "Right: ", "")
RawText := StrReplace(RawText, "Bottom: ", "")
RawText := StrReplace(RawText, "Width: ", ",")

RawText := StrReplace(RawText,"`r","")
RawText := StrReplace(RawText,"`n","")

; Cast from string to int
MonitorWidth := MonitorWidth
MonitorHeight := MonitorHeight

; Split text entered by comma, and add those numbers to a variable
Loop, Parse, RawText, % ","
{
	if (A_Index = 1) {
		NumbersList := A_LoopField
	} else if (A_Index = 2) {
		NumbersList := NumbersList . "," . A_LoopField
	} else if (A_Index = 3) {
		NumbersList := NumbersList . "," . MonitorWidth-A_LoopField
	} else if (A_Index = 4) {
		NumbersList := NumbersList . "," . MonitorHeight-A_LoopField
	}
}
Clipboard = %NumbersList%
Msgbox,
(
Crop Margin: %NumbersList%`n`nThese values have automatically been copied to your clipboard.`n`nIn ShareX, go to Hotkey Settings, click the gear icon on the
workflow you desire, Image > Override image settings, Image > Effects > Image effects configuration, Effects > plus sign > Manipulations >
Crop, paste and replace the values to the right of margin.
)

Return

Exitapp
GuiClose:
ExitApp
return