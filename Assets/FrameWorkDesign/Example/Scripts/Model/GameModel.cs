﻿namespace FrameWorkDesign.Example
{
    public interface IGameModel : IModel
    {
        public BindableProperty<int> KillCount { get; }
        public BindableProperty<int> Score { get; }
        public BindableProperty<int> Gold { get; }
        public BindableProperty<int> BestScore { get; }
    }
    public class GameModel : AbstractModel, IGameModel
    {
        public BindableProperty<int> KillCount { get; } = new BindableProperty<int>()
        {
            Value = 0
        };
        public BindableProperty<int> Gold { get; } = new();
        public BindableProperty<int> Score { get; } = new();
        public BindableProperty<int> BestScore { get; } = new();

        protected override void OnInit()
        {
            var storage = this.GetUtility<IStorage>();
            BestScore.Value = storage.LoadInt(nameof(BestScore), 0);
            BestScore.Register(v => storage.SaveInt(nameof(BestScore), v));
        }
    }
}
