:: use /Gec option for compatibility mode (required by some .fx files)
echo ******* Compiling Pixel Shaders *******
"%DXSDK_DIR%\Utilities\bin\x86\fxc" /T ps_3_0 /E main /Fo ".\effect\GrayFilter.ps" ".\effect\GrayFilter.fx"
echo ******* Compiling Pixel Shaders Completed *******
