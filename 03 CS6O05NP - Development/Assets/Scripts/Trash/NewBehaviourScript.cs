

//// Lets player choose their action
//using UnityEngine.SceneManagement;
//using UnityEngine;

//void PlayerTurn()
//{
//    Dialouge("Choose an action");
//}

//// Damage calculation is done and looks for win/lose state
//IEnumerator PlayerAttack()
//{
//    bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

//    enemyHud.SetHp(enemyUnit.currentHp);
//    Dialouge("You did damage");

//    yield return new WaitForSeconds(2f);

//    if (isDead)
//    {
//        state = BattleState.WON;
//        EndBattle();
//    }
//    else
//    {
//        state = BattleState.ENEMYTURN;
//        StartCoroutine(EnemyTurn());
//    }
//}

//// Logic for player input: Physical Attack
//public void OnButtonPressedAttack()
//{
//    if (state != BattleState.PLAYERTURN)
//        return;

//    StartCoroutine(PlayerAttack());
//}

//// Logic for player input: Item
//public void OnButtonPressedItem()
//{
//    if (state != BattleState.PLAYERTURN)
//        return;

//    StartCoroutine(PlayerItems());
//}

//// Logic for Item
//IEnumerator PlayerItems()
//{
//    playerUnit.Heal(50);

//    playerHud.SetHp(playerUnit.currentHp);
//    Dialouge("You Healed Yourself");
//    yield return new WaitForSeconds(2f);

//    state = BattleState.ENEMYTURN;
//    StartCoroutine(EnemyTurn());

//}

//// Ends the battle and shows dialouge if they w/l
//void EndBattle()
//{
//    Load load = new Load();
//    if (state == BattleState.WON)
//    {
//        Dialouge("You won the battle");
//        SceneManager.LoadScene(1);
//    }
//    else if (state == BattleState.LOST)
//    {
//        Dialouge("You lost");
//        SceneManager.LoadScene(0);
//    }
//}

//// Logic for enemy turn
//IEnumerator EnemyTurn()
//{
//    Dialouge(enemyUnit.unitName + "attacks");

//    bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

//    playerHud.SetHp(playerUnit.currentHp);
//    yield return new WaitForSeconds(2f);

//    if (isDead)
//    {
//        state = BattleState.LOST;
//    }
//    else
//    {
//        state = BattleState.PLAYERTURN;
//        PlayerTurn();
//    }
//}