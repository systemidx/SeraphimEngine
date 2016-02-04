using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SeraphimEngine.Managers.Asset;

namespace SeraphimEngine.Managers.Gui
{
    public class GuiManager : Manager<GuiManager>, IGuiManager
    {
        public SpriteFont GuiFont;
        public Texture2D GuiCursorTexture;
        public Texture2D GuiContainerTexture;
        public SoundEffect GuiRolloverSound;
        public SoundEffect GuiSelectSound;
        public SoundEffectInstance GuiRolloverSoundInstance;
        public SoundEffectInstance GuiRolloverSelectInstance;

        public override bool IsInitialized { get; protected set; }

        public override void Initialize(ContentManager content, GraphicsDevice graphics)
        {
            GuiFont = AssetManager.Instance.GetAsset<SpriteFont>("fonts", "default");

            GuiContainerTexture = AssetManager.Instance.GetAsset<Texture2D>("textures/menu", "menu");
            GuiCursorTexture = AssetManager.Instance.GetAsset<Texture2D>("textures/menu", "cursor");

            GuiRolloverSound = AssetManager.Instance.GetAsset<SoundEffect>("sounds", "rollover");
            GuiRolloverSoundInstance = GuiRolloverSound.CreateInstance();

            GuiSelectSound = AssetManager.Instance.GetAsset<SoundEffect>("sounds", "select");
            GuiRolloverSelectInstance = GuiSelectSound.CreateInstance();

            IsInitialized = true;
        }

        public void PlaySoundEffect(SoundEffectInstance instance)
        {
            instance?.Stop();
            instance?.Play();
        }
    }
}
