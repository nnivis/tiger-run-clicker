namespace TigerClicker.CodeBase.Services.StateMachine
{
    public class MainSceneMode : StateModeBehavior
    {
        public void GotoMascotMenu()
        {
            ChangeState<MascotMenuState>();
        }

        public void GotoMenu()
        {
            ChangeState<MenuState>();
        }

        public void GotoSettings()
        {
            ChangeState<SettingsState>();
        }

        public void GotoRecord()
        {
            ChangeState<RecordState>();
        }
        public void GotoLoad()
        {
            ChangeState<LoadState>();
        }
        public void GotoGame()
        {
            ChangeState<GameState>();
        }
    }
}
