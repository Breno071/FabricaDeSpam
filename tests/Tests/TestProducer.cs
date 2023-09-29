namespace Tests
{
    using Moq;
    public class TestProducer
    {
        /// <summary>
        /// Padr�o de nome : Given_When_Then
        /// Exemplo :
        /// Given - Dado o construtor
        /// When - Quando n�o tem Kafka
        /// Then - N�o deve lan�ar exce��o
        /// </summary>
        [Fact]
        public async void Constructor_WithoutKafka_ShouldNotThrowException()
        {
            var constructorTask = Task.Run(() => new KafkaProducerService());

            await Task.Delay(5000);

            Assert.Null(Record.Exception(constructorTask.Wait));
        }
    }
}