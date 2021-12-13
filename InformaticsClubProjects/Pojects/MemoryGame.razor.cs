namespace InformaticsClubProjects.Pojects
{

    public partial class MemoryGame
    {

        Model model = new Model();
        List<Colors> sequence = new List<Colors>();
        List<Colors> playerSequence = new List<Colors>();
        GameStates gameState = GameStates.Intro;
        bool choosingColor = false;
        int sequenceIndex = 0;
        string msg = "";

        async Task playSequence()
        {
            await Task.Run(async () =>
            {
                await InvokeAsync(StateHasChanged);
                await Task.Delay(500);
                model.SetColorActive(sequence[sequenceIndex], true);
                await Task.Run(async () =>
                {
                    await InvokeAsync(StateHasChanged);
                    await Task.Delay(500);
                    model.SetColorActive(sequence[sequenceIndex], false);
                    sequenceIndex++;
                    if (sequenceIndex < sequence.Count)
                    {
                        await playSequence();
                    }
                    else
                    {
                        sequenceIndex = 0;
                        choosingColor = false;
                        msg = localizer["project7.msg1"];
                    }
                    await InvokeAsync(StateHasChanged);
                });

            });
        }

        async Task chooseRandomColor()
        {
            msg = localizer["project7.msg2"];
            choosingColor = true;
            sequence.Add(RandomColor());
            await playSequence();
        }

        Colors RandomColor()
        {
            Array values = Enum.GetValues(typeof(Colors));
            Random random = new Random();
            return (Colors)values.GetValue(random.Next(values.Length));
        }
        void onMouseDown(Colors color)
        {
            if (!choosingColor)
            {
                model.SetColorActive(color, true);
                StateHasChanged();
                playerSequence.Add(color);
                for (var i = 0; i < playerSequence.Count; i++)
                {
                    if (playerSequence[i] != sequence[i])
                    {
                        gameState = GameStates.GameOver;
                        model.SetAllInactive();
                        StateHasChanged();
                        return;
                    }
                }
            }
        }

        async Task onMouseUp(Colors color)
        {
            if (!choosingColor)
            {
                model.SetColorActive(color, false);
                StateHasChanged();
                if (playerSequence.Count == sequence.Count)
                {
                    playerSequence.Clear();
                    await chooseRandomColor();
                }
            }
        }

        void onMouseEnter(Colors color)
        {
            if (!choosingColor && gameState == GameStates.Running)
            {
                model.SetColorHover(color, true);
            }
        }

        void onMouseLeave(Colors color)
        {
            model.SetColorHover(color, false);
            model.SetColorActive(color, false);
        }

        bool activeState(Colors color)
        {
            return model.GetColorActive(color);
        }

        bool hoverState(Colors color)
        {
            return model.GetColorHover(color);
        }

        async Task restart()
        {
            model = new Model();
            msg = "";
            sequenceIndex = 0;
            playerSequence.Clear();
            sequence.Clear();
            choosingColor = false;
            gameState = GameStates.Running;
            StateHasChanged();
            await chooseRandomColor();
        }
    }
    enum Colors { Red, Green, Black, Orange }
    enum GameStates { Intro, Running, GameOver }
    struct Color
    {
        public readonly string color;
        public bool active = false;
        public bool hover = false;
        public Color(string c) { color = c; }
    }
    struct Model
    {
        public Color red = new Color("red");
        public Color green = new Color("green");
        public Color black = new Color("black");
        public Color orange = new Color("orange");
        public string GetColorStyle(Colors color)
        {
            var back = "background-color:";
            switch (color)
            {
                case Colors.Red: back += red.color + ";"; break;
                case Colors.Green: back += green.color + ";"; break;
                case Colors.Black: back += black.color + ";"; break;
                case Colors.Orange: back += orange.color + ";"; break;
            }
            return back;
        }
        public void SetColorActive(Colors color, bool activeState)
        {
            switch (color)
            {
                case Colors.Red: red.active = activeState; break;
                case Colors.Green: green.active = activeState; break;
                case Colors.Black: black.active = activeState; break;
                case Colors.Orange: orange.active = activeState; break;
                default: break;
            }
        }
        public bool GetColorActive(Colors color)
        {
            switch (color)
            {
                case Colors.Red: return red.active;
                case Colors.Green: return green.active;
                case Colors.Black: return black.active;
                case Colors.Orange: return orange.active;
                default: return false;
            }
        }
        public void SetAllInactive()
        {
            red.active = false;
            green.active = false;
            black.active = false;
            orange.active = false;
        }
        public void SetColorHover(Colors color, bool hoverState)
        {
            switch (color)
            {
                case Colors.Red: red.hover = hoverState; break;
                case Colors.Green: green.hover = hoverState; break;
                case Colors.Black: black.hover = hoverState; break;
                case Colors.Orange: orange.hover = hoverState; break;
                default: break;
            }
        }

        internal bool GetColorHover(Colors color)
        {
            switch (color)
            {
                case Colors.Red: return red.hover;
                case Colors.Green: return green.hover;
                case Colors.Black: return black.hover;
                case Colors.Orange: return orange.hover;
                default: return false;
            }
        }
    }
}
