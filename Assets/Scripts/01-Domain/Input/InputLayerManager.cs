using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain {
    public class InputLayerManager {

        private readonly Dictionary<InputLayerType, List<IInputLayer>> _layerLists =
            Enum.GetValues(typeof(InputLayerType))
                .Cast<InputLayerType>()
                .ToDictionary(type => type, _ => new List<IInputLayer>());

        private static readonly InputLayerType[] EVALUATION_ORDER = {
            InputLayerType.SystemOverlay,
            InputLayerType.Popups,
            InputLayerType.HUD,
            InputLayerType.Gameplay
        };

        private bool _dirtyFlag = false;

        private List<IInputLayer> _sortedLayers = new();

        public IReadOnlyCollection<IInputLayer> LayersTopDown {
            get {
                if(_dirtyFlag) RecalculateSortedList();
                return _sortedLayers;
            }
        }

        public InputLayerManager(IEnumerable<IInputLayer> defaultLayers) {
            foreach(var layer in defaultLayers) PushLayer(layer);
        }

        public void PushLayer(IInputLayer layer) {
            _layerLists[layer.Type].Insert(0, layer);
            _dirtyFlag = true;
        }

        public void PopLayer(IInputLayer layer) {
            foreach(var list in _layerLists.Values) {
                int amount = list.RemoveAll(x => x == layer);
            }
            _sortedLayers.RemoveAll(x => x == layer);
        }

        private void RecalculateSortedList() {
            _sortedLayers.Clear();
            foreach(var type in EVALUATION_ORDER) {
                for(int i = 0; i < _layerLists[type].Count; i++) {
                    _sortedLayers.Add(_layerLists[type][i]);
                }
            }
            _dirtyFlag = false;
        }
    }
}