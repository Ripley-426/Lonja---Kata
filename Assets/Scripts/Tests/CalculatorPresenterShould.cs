using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;
using Scenes.Calculator_Scene.MVP;

namespace Tests
{
    public class CalculatorPresenterShould
    {
        
        private ICalculatorPresenter _presenter;
        private ICalculatorView _view;

        [SetUp]
        public void Setup()
        {
            _view = Substitute.For<ICalculatorView>();
            _presenter = new CalculatorPresenter(_view);
        }
        
        [Test]
        public void EnableNewFishPanelInViewWhenAddingANewFish()
        {
            _presenter.OpenAddNewFishInput();
            
            _view.Received(1).EnableFishInputPanel();
        }
    }
}
