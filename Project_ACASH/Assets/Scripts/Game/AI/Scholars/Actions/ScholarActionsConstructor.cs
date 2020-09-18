using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AI.Scholars.Actions.Operations;
using AI.Scholars.Actions.Operations.Types;
using AI.Scholars.Items;
using Objects.Organization.Places;
using Vkimow.Tools.Single;

namespace AI.Scholars.Actions
{
    public class ScholarActionsConstructor
    {
        private readonly Scholar _scholar;
        public ScholarActionsConstructor(Scholar scholar)
        {
            _scholar = scholar;
        }

        public List<IScholarOperation> Create(string action)
        {
            var parameters = action.Split('_');

            if (parameters.Length != 2)
                throw new Exception($"Неправильное действие \"{action}\" | Length = {parameters.Length}");

            switch (parameters[0])
            {
                case "GoTo":
                    {
                        return GoTo(parameters[1]);
                    }
                case "Cheat":
                    {
                        return Cheat(parameters[1]);
                    }
                case "Special":
                    {
                        return Special(parameters[1]);
                    }
            }

            throw new Exception($"Неизвествное действие \"{parameters[0]}\" | Full = \"{action}\"");
        }

        private List<IScholarOperation> GoTo(string parameter)
        {
            switch (parameter)
            {
                case "Toilet":
                case "Sink":
                case "Hallway":
                    {
                        var place = _scholar.ClassRoom.PlaceAgent.GetRandomFreePlace(parameter);

                        return new List<IScholarOperation>()
                            {
                                new GoTo(_scholar, place)
                            };
                    }
                case "Desk":
                    {
                        return new List<IScholarOperation>()
                            {
                                new GoToDesk(_scholar)
                            };
                    }
                case "DockStation":
                    {
                        return new List<IScholarOperation>()
                            {
                                new GoToDockStation(_scholar)
                            };
                    }
            }

            throw new Exception($"Неизвествный параметр \"{parameter}\" дейсвия \"GoTo\"");
        }


        private List<IScholarOperation> Special(string parameter)
        {
            switch (parameter)
            {
                case "Pee":
                    {
                        return new List<IScholarOperation>()
                            {
                                new Wait(_scholar, 5f)
                            };
                    }
                case "WashHands":
                    {
                        return new List<IScholarOperation>()
                            {
                                new Wait(_scholar, 5f)
                            };
                    }
                case "Rest":
                    {
                        return new List<IScholarOperation>()
                            {
                                new Wait(_scholar, 5f)
                            };
                    }
                case "Talk":
                    {
                        return null;
                    }
            }

            throw new Exception($"Неизвествный параметр \"{parameter}\" дейсвия \"Special\"");
        }

        private List<IScholarOperation> Cheat(string parameter)
        {
            switch (parameter)
            {
                case "Note":
                case "Phone":
                case "Calculator":
                    {
                        ScholarItem.Type item;

                        if (Enum.TryParse<ScholarItem.Type>(parameter, out item))
                            break;

                        return new List<IScholarOperation>()
                            {
                                new Take(_scholar, item),
                                new Wait(_scholar, 5f),
                                new Put(_scholar)
                            };
                    }
            }

            throw new Exception($"Неизвествный параметр \"{parameter}\" дейсвия \"Cheat\"");
        }

    }
}
