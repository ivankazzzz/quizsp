﻿using Fluxor;
using Microsoft.AspNetCore.Components;
using ThePeriodicTableOfElementsGame.GamePlay.ElementsMatchGame;
using ThePeriodicTableOfElementsGame.GamePlay.ElementsMatchGame.Actions;
using ThePeriodicTableOfElementsGame.GamePlay.Navigation;
using ThePeriodicTableOfElementsGame.GamePlay.Navigation.Actions;
using ThePeriodicTableOfElementsGame.GamePlay.PeriodicTableData;
using ThePeriodicTableOfElementsGame.Web.Extensions;

namespace ThePeriodicTableOfElementsGame.Web.Scenes
{
	public partial class ElementsMatchGame
	{
		[Inject]
		private IState<ElementsMatchGameState> GameState { get; set; }

		[Inject]
		private IState<NavigationState> NavigationState { get; set; }

		[Inject]
		private IDispatcher Dispatcher { get; set; }

		protected override void OnAfterRender(bool firstRender)
		{
			base.OnAfterRender(firstRender);
			if (firstRender)
				Dispatcher.Dispatch(new StartGameAction());
		}

		private string GetUIStatusCss()
		{
			if (NavigationState.Value.Scene == SceneType.ElementsMatchGameOver)
				return "--game-over";
			if (NavigationState.Value.Scene == SceneType.TransitionFromElementsMatchGameToGameOver)
				return "--game-over-sequence";
			return null;
		}

		private string GetElementGroupAsCssClass() =>
			GameState.Value.ExpectedElement is null || !GameState.Value.ShowElementGroup
			? ""
			: ElementGroupExtensions.GetAsCssClass(
					TableOfElementsData.ElementByNumber[GameState.Value.ExpectedElement.Value].Group);

		private void GoToMainMenu()
		{
			Dispatcher.Dispatch(new NavigateAction(SceneType.MainMenu));
		}
	}
}