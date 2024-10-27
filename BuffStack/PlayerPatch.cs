using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace BuffStack;

/*
 * UncappedSoda was used as a reference: https://github.com/sophiethefox/UncappedSoda-Webfishing/blob/main/UncappedSoda/PlayerPatch.cs
 */
public class PlayerPatch : IScriptMod
{

    public bool ShouldRun(string path) => path == "res://Scenes/Entities/Player/player.gdc";

    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens)
    {
        //  "catch":
        //      catch_drink_timer =
        var catchMatch = new MultiTokenWaiter([
            t => t is ConstantToken { Value: StringVariant { Value: "catch" } },
            t => t.Type is TokenType.Colon,
            t => t.Type is TokenType.Newline,
            t => t is IdentifierToken { Name: "catch_drink_timer" },
            t => t.Type is TokenType.OpAssign
        ]);

        //  "catch_big":
        //      catch_drink_timer =
        var catchBigMatch = new MultiTokenWaiter([
            t => t is ConstantToken { Value: StringVariant { Value: "catch_big" } },
            t => t.Type is TokenType.Colon,
            t => t.Type is TokenType.Newline,
            t => t is IdentifierToken { Name: "catch_drink_timer" },
            t => t.Type is TokenType.OpAssign
        ]);

        //  "catch_deluxe":
        //      catch_drink_timer =
        var catchDeluxeMatch = new MultiTokenWaiter([
            t => t is ConstantToken { Value: StringVariant { Value: "catch_deluxe" } },
            t => t.Type is TokenType.Colon,
            t => t.Type is TokenType.Newline,
            t => t is IdentifierToken { Name: "catch_drink_timer" },
            t => t.Type is TokenType.OpAssign
        ]);

        var newlineConsumer = new TokenConsumer(t => t.Type is TokenType.Newline);

        foreach (var token in tokens)
        {
            if (newlineConsumer.Check(token))
            {
                continue;
            }

            if (newlineConsumer.Ready)
            {
                yield return token;
                newlineConsumer.Reset();
            }

            if (catchMatch.Check(token))
            {
                yield return token;
                yield return new IdentifierToken("catch_drink_timer");
                yield return new Token(TokenType.OpAdd);
                yield return new ConstantToken(new IntVariant(18000));

                // Reset since this matches multiple times
                catchMatch.Reset();
                // Consume the "disabled or refreshing" and wait until newline
                newlineConsumer.SetReady();
            }
            else if (catchBigMatch.Check(token))
            {
                yield return token;
                yield return new IdentifierToken("catch_drink_timer");
                yield return new Token(TokenType.OpAdd);
                yield return new ConstantToken(new IntVariant(18000));

                // Reset since this matches multiple times
                catchBigMatch.Reset();
                // Consume the "disabled or refreshing" and wait until newline
                newlineConsumer.SetReady();
            }
            else if (catchDeluxeMatch.Check(token))
            {
                yield return token;
                yield return new IdentifierToken("catch_drink_timer");
                yield return new Token(TokenType.OpAdd);
                yield return new ConstantToken(new IntVariant(18000));

                // Reset since this matches multiple times
                catchDeluxeMatch.Reset();
                // Consume the "disabled or refreshing" and wait until newline
                newlineConsumer.SetReady();
            }
            else
            {
                yield return token;
            }
        }
    }
}

