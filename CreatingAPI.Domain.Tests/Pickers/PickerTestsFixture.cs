using Bogus;
using CreatingAPI.Domain.Pickers;
using CreatingAPI.Domain.Pickers.Interfaces;
using CreatingAPI.Domain.Tests.Base;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace CreatingAPI.Domain.Tests.Pickers
{
    [CollectionDefinition(nameof(PickerTestsFixtureCollection))]
    public class PickerTestsFixtureCollection : ICollectionFixture<PickerTestsFixture>
    {
    }

    public class PickerTestsFixture : BaseTestsFixture, IDisposable
    {
        private const string TITLE_GENERATES_DATABASE_ERROR_PICKER = "DATABASE ERROR";
        private const int ID_INEXISTENT_PICKER = 1;

        public int GetInexistentPickerId()
        {
            return ID_INEXISTENT_PICKER;
        }
        public Picker GetFakePicker()
        {
            var fakePicker = new Faker<Picker>()
                .CustomInstantiator(f => new Picker(f.Lorem.Sentence(), f.Random.Int(1), f.Random.Bool()));

            return fakePicker;
        }

        public Picker GetPickerGeneratesDatabaseError()
        {
            var fakePicker = new Faker<Picker>()
                .CustomInstantiator(f => new Picker(TITLE_GENERATES_DATABASE_ERROR_PICKER, f.Random.Int(1), f.Random.Bool()));

            return fakePicker;
        }

        public IEnumerable<PickerTopic> GetFakeTopics()
        {
            return new Faker<PickerTopic>()
                .CustomInstantiator(f => new PickerTopic(f.Lorem.Sentence())).Generate(10);
        }

        public Mock<IPickerRepository> GetPickerMockRepository()
        {
            var repositoryMock = new Mock<IPickerRepository>(MockBehavior.Loose);
            repositoryMock.Setup(rm => rm.CreateAsync(It.Is<Picker>(p => p.Title != TITLE_GENERATES_DATABASE_ERROR_PICKER))).ReturnsAsync(GetRandomInt());
            repositoryMock.Setup(rm => rm.CreateAsync(It.Is<Picker>(p => p.Title == TITLE_GENERATES_DATABASE_ERROR_PICKER))).ReturnsAsync(0);

            repositoryMock.Setup(rm => rm.UpdateAsync(It.Is<Picker>(p => p.Title != TITLE_GENERATES_DATABASE_ERROR_PICKER))).ReturnsAsync(true);
            repositoryMock.Setup(rm => rm.UpdateAsync(It.Is<Picker>(p => p.Title == TITLE_GENERATES_DATABASE_ERROR_PICKER))).ReturnsAsync(false);

            repositoryMock.Setup(rm => rm.DeleteAsync(It.IsAny<Picker>())).ReturnsAsync(true);

            repositoryMock.Setup(rm => rm.GetAsync(It.Is<int>(i => i != ID_INEXISTENT_PICKER))).ReturnsAsync(GetFakePicker());
            repositoryMock.Setup(rm => rm.GetAsync(It.Is<int>(i => i == ID_INEXISTENT_PICKER))).ReturnsAsync((Picker)null);

            return repositoryMock;
        }

        public void Dispose()
        {
        }
    }
}
