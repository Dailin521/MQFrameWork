﻿namespace FrameWorkDesign.Example
{
    public class PointGame : Architecture<PointGame>
    {
        protected override void Init()
        {
            RegisterModel<IGameModel>(new GameModel());
        }
    }
}
