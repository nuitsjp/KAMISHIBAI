using Microsoft.Maui.Controls;

namespace Kamishibai.Maui.Mvvm
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="TPage"></typeparam>
	public abstract class AnimatableNavigation<TPage> : NavigationBehavior<TPage> where TPage : Page
    {
        public static readonly BindableProperty AnimatedProperty =
            BindableProperty.Create(nameof(Animated), typeof(bool), typeof(AnimatableNavigation<TPage>), true);

        public bool Animated
        {
            get => (bool)GetValue(AnimatedProperty);
            set => SetValue(AnimatedProperty, value);
        }
    }
}
