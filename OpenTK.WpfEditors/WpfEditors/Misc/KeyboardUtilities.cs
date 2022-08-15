using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace OpenTK.WpfEditors;

public static class KeyboardUtilities
{
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    internal static bool IsKeyModifyingPopupState(KeyEventArgs e)
    {
        return ((Keyboard.Modifiers & ModifierKeys.Alt) == ModifierKeys.Alt && (e.SystemKey == Key.Down || e.SystemKey == Key.Up)) || e.Key == Key.F4;
    }
}
