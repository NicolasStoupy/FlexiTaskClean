namespace WebApp.Services
{
    public class ThemeService
    {
  
      
        public bool IsDarkMode { get; set; } = false;
        public event Action OnChange;

        public void ToggleDarkMode()
        {
            IsDarkMode = !IsDarkMode;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
