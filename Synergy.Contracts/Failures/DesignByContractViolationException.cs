using System;
using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace Synergy.Contracts
{
    /// <summary>
    /// Wyj¹tek rzucany g³ównie przez klasê <see cref="Fail"/>. Mówi on o tym, ¿e œwiat zewnêtrzny
    /// dla naszej logiki nie spe³ni³ kontraktu jaki nasz kod zak³ada³ - np. przyszed³ null do metody
    /// a my nie zak³adaliœmy, ¿e mo¿e przyjœæ. Nasz kod zak³ada³, ¿e jest uruchomiony na serwerze a 
    /// okaza³o siê inaczej, itd. <br/>
    /// 
    /// Ten exception nigdy nie powinien byæ rzucany. Jeœli go widzisz 
    /// oznacza to, ¿e jest coœ nie tak ze œwiatem zewnêtrznym, który nie spe³nia kontraktu danej metody.
    /// </summary>
    [Serializable]
    public class DesignByContractViolationException : Exception
    {
        /// <summary>
        /// Wyj¹tek rzucany g³ównie przez klasê <see cref="Fail"/>. Mówi on o tym, ¿e œwiat zewnêtrzny
        /// dla naszej logiki nie spe³ni³ kontraktu jaki nasz kod zak³ada³ - np. przyszed³ null do metody
        /// a my nie zak³adaliœmy, ¿e mo¿e przyjœæ. Nasz kod zak³ada³, ¿e jest uruchomiony na serwerze a 
        /// okaza³o siê inaczej, itd. <br/>
        /// 
        /// Ten exception nigdy nie powinien byæ rzucany. Jeœli go widzisz 
        /// oznacza to, ¿e jest coœ nie tak ze œwiatem zewnêtrznym, który nie spe³nia kontraktu danej metody.
        /// </summary>
        public DesignByContractViolationException()
        {
        }

        /// <summary>
        /// Wyj¹tek rzucany g³ównie przez klasê <see cref="Fail"/>. Mówi on o tym, ¿e œwiat zewnêtrzny
        /// dla naszej logiki nie spe³ni³ kontraktu jaki nasz kod zak³ada³ - np. przyszed³ null do metody
        /// a my nie zak³adaliœmy, ¿e mo¿e przyjœæ. Nasz kod zak³ada³, ¿e jest uruchomiony na serwerze a 
        /// okaza³o siê inaczej, itd. <br/>
        /// 
        /// Ten exception nigdy nie powinien byæ rzucany. Jeœli go widzisz 
        /// oznacza to, ¿e jest coœ nie tak ze œwiatem zewnêtrznym, który nie spe³nia kontraktu danej metody.
        /// </summary>
        public DesignByContractViolationException([NotNull] string message) : base(message)
        {
        }

        /// <summary>
        /// Konstruktor s³u¿¹cy do deserializacji. Bez niego ten wyj¹tek nie jest w stanie sie zdeserializowaæ.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected DesignByContractViolationException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
    }
}