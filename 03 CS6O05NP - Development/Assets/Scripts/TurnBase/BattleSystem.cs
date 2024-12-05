/* 
Checks Battle State and Makes calculation accordingly until (Win or Lose) State

Works Remaining:        - Skills 
                        - Animation
                        - Speed Calculation
                        - Defense Calculation
                        - Counter-State (For Future)
Problems:               - NA
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public enum BattleState { START, PLAYERTURN, PLAYERDAMAGECALCULATION, ENEMYTURN, ENEMYDAMAGECALCULATION, WON, LOST, RETREAT}
public class BattleSystem : MonoBehaviour
{
    public BattleState state;

    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public GameObject ItemCanvas;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    public TextMeshProUGUI dialougeText;

    public BattleHUD playerHud;
    public BattleHUD enemyHud;

    Unit playerUnit;
    Unit enemyUnit;

    int itemDamage = 200;

    //Start of the battle
    private void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    //All prefabs and animation are loaded and dialouge is given
    IEnumerator SetupBattle()
    {
        ItemCanvas.SetActive(false);

        GameObject playerGo = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGo.GetComponent<Unit>();

        GameObject enemyGo = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGo.GetComponent<Unit>();

        Dialouge("A wild " + enemyUnit.unitName + " approaches...");

        playerHud.SetHUD(playerUnit);
        enemyHud.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    // All dialouge are converted here
    private void Dialouge(string text)
    {
        dialougeText.text = text;
    }

    void PlayerTurn()
    {
        Dialouge("Choose an action");
    }

    // Logic for player input: Physical Attack
    public void OnButtonPressedAttack()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        state = BattleState.PLAYERDAMAGECALCULATION;
        StartCoroutine(PlayerAttack(playerUnit.damage));
    }

    // Logic for player input: Item
    public void OnButtonPressedItem()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        ItemCanvas.SetActive(!ItemCanvas.activeSelf);
    }

    // Logic for player input: Run
    public void OnButtonPresedRun()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        state = BattleState.PLAYERDAMAGECALCULATION;
        RunManagement();
    }

    public void OnButtonPressedDamageItem()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        state = BattleState.PLAYERDAMAGECALCULATION;

        ItemCanvas.SetActive(false);
        StartCoroutine(PlayerAttack(itemDamage));
    }

    public void OnButtonPressedHealItem()
    {
        if (state != BattleState.PLAYERTURN) 
            return;

        state = BattleState.PLAYERDAMAGECALCULATION;
        ItemCanvas.SetActive(false);
        StartCoroutine(Heal());
    }

    // Checks if player will be able to run or not
    public void RunManagement()
    {
        if (playerUnit.speed > enemyUnit.speed)
        {
            state = BattleState.RETREAT;
            EndBattle();
        } 
        else
        {
            Dialouge("CouldnotRunAway");
            state = BattleState.ENEMYTURN;
        }
    }

    // Logic for Item
    public IEnumerator Heal()
    {
        playerUnit.Heal(50);

        playerHud.SetHp(playerUnit.currentHp);
        Dialouge("You Healed Yourself");
        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    // Damage calculation is done and looks for win/lose state
    IEnumerator PlayerAttack(int damage)
    {
        bool isDead = enemyUnit.TakeDamage(damage);

        enemyHud.SetHp(enemyUnit.currentHp);
        Dialouge("You did damage");

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    // Ends the battle and shows dialouge if they w/l
    void EndBattle()
    {
        Load load = new Load();
        if (state == BattleState.WON)
        {
            Dialouge("You won the battle");
            SceneManager.LoadScene(1);
        }
        else if (state == BattleState.LOST)
        {
            Dialouge("You lost");
            SceneManager.LoadScene(0);
        }
        else if (state == BattleState.RETREAT)
        {
            Dialouge("Successfully Run Away");
            SceneManager.LoadScene(1);
        }
    }

    // Logic for enemy turn
    IEnumerator EnemyTurn()
    {
        Dialouge(enemyUnit.unitName + "attacks");

        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

        playerHud.SetHp(playerUnit.currentHp);
        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.LOST;
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }
}
