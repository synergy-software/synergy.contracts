using System;
using System.Diagnostics;
using JetBrains.Annotations;

namespace Synergy.Contracts
{
    public static partial class Fail
    {
        private const string NotCastableMessage = "Expected object of type '{0}' but was '{1}'";

        /// <summary>
        /// Rzuca wyj¹tek gdy testowany obiekt nie daje siê zrzutowaæ na wymagany typ.
        /// Do tej metody mo¿na przekazaæ <see langword="null" /> - wtedy nie jest sprawdzane rzutowanie.
        /// </summary>
        [DebuggerStepThrough]
        [ContractAnnotation("value: null => null; value: notnull => notnull")]
        [CanBeNull]
        [AssertionMethod]
        public static T AsOrFail<T>([CanBeNull] this object value)
        {
            IfNotCastable<T>(value, NotCastableMessage, typeof(T), value);

            return (T)value;
        }

        /// <summary>
        /// Rzuca wyj¹tek gdy testowany obiekt nie daje siê zrzutowaæ na wymagany typ.
        /// Do tej metody NIE mo¿na przekazaæ <see langword="null" />.
        /// </summary>
        [DebuggerStepThrough]
        [NotNull]
        [AssertionMethod]
        public static T CastOrFail<T>([CanBeNull] this object value)
        {
            IfNull(value, NotCastableMessage, typeof (T), "null");
            IfNotCastable<T>(value, NotCastableMessage, typeof(T), value);
            return (T) value;
        }

        /// <summary>
        /// Rzuca wyj¹tek gdy testowany obiekt nie daje siê zrzutowaæ na wymagany typ.
        /// Do tej metody mo¿na przekazaæ <see langword="null" /> - wtedy nie jest sprawdzane rzutowanie.
        /// </summary>
        [DebuggerStepThrough]
        [StringFormatMethod("message")]
        [AssertionMethod]
        public static void IfNotCastable([CanBeNull] object value, [NotNull] Type expectedType, [NotNull] string message, [NotNull] params object[] args)
        {
            RequiresMessage(message, args);

            if (value == null)
                return;

            if (expectedType.IsInstanceOfType(value) == false)
                throw Because(message, args);
        }

        /// <summary>
        /// Sprawdza czy dany obiekt jest danego typu (czy jest rzutowalny).
        /// Jeœli nie jest rzucany jest Exception o podanym komunikacie.
        /// Do tej metody mo¿na przekazaæ <see langword="null" /> - wtedy nie jest sprawdzane rzutowanie.
        /// </summary>
        /// <typeparam name="T">Oczekiwany typ obiektu</typeparam>
        /// <param name="toCheck">Obiekt do sprawdzenia</param>
        /// <param name="message">Komunikat</param>
        /// <param name="args">Argumenty komunikatu</param>
        [DebuggerStepThrough]
        [StringFormatMethod("message")]
        [AssertionMethod]
        public static void IfNotCastable<T>([CanBeNull] object toCheck, [NotNull] string message, [NotNull] params object[] args)
        {
            IfNotCastable(toCheck, typeof (T), message, args);
        }

        /// <summary>
        /// Sprawdza czy dany obiekt jest danego typu (czy jest rzutowalny).
        /// Jeœli nie jest rzucany jest Exception o podanym komunikacie.
        /// Do tej metody NIE mo¿na przekazaæ <see langword="null" />.
        /// </summary>
        [DebuggerStepThrough]
        [StringFormatMethod("message")]
        [AssertionMethod]
        public static void IfNullOrNotCastable<T>(
            [CanBeNull] object value,
            [NotNull] string message,
            [NotNull] params object[] args)
        {
            //
            // WARN: Powy¿ej na argumencie (value) specjalnie jest [CanBeNull] - bez tego R# zg³asza b³êdy w miejscach, 
            //       w których chcemy przetestowaæ brak nullowatoœci wbrew deklarowanemu kontraktowi 
            //

            RequiresMessage(message, args);

            IfNull(value, message, args);
            IfNotCastable(value, typeof (T), message, args);
        }

        /// <summary>
        /// Sprawdza czy dany obiekt jest danego typu (czy jest rzutowalny).
        /// Jeœli nie jest rzucany jest Exception o podanym komunikacie.
        /// Do tej metody NIE mo¿na przekazaæ <see langword="null" />.
        /// </summary>
        [Obsolete("Use Fail.IfNullOrNotCastable<T>() overload with message")]
        [DebuggerStepThrough]
        [AssertionMethod]
        public static void IfNullOrNotCastable<T>([CanBeNull] object value)
        {
            //
            // WARN: Powy¿ej na argumencie (value) specjalnie jest [CanBeNull] - bez tego R# zg³asza b³êdy w miejscach, 
            //       w których chcemy przetestowaæ brak nullowatoœci wbrew deklarowanemu kontraktowi 
            //

            IfNull(value, NotCastableMessage, typeof (T), "null");
            IfNotCastable(value, typeof (T));
        }

        [DebuggerStepThrough]
        private static void IfNotCastable([CanBeNull] object value, [NotNull] Type expectedType)
        {
            IfNotCastable(value, expectedType, NotCastableMessage, expectedType, value);
        }
    }
}