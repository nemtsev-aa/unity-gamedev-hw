using System;
using UnityEngine;

namespace PresentationModel {
    public interface IUserInfoViewModel : IViewModel, IDisposable {
        string UserName { get; }
        string Description { get; }
        Sprite Icon { get; }
    }
}