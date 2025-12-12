const startBtn = document.getElementById('start-btn');
const dungeon = document.getElementById('dungeon');

const suits = ['♠', '♣', '♥', '♦'];
const values = ['A', '2', '3', '4', '5', '6', '7', '8', '9', '10', 'J', 'Q', 'K'];

function createDeck() {
  const deck = [];

  for (let suit of suits) {
    for (let value of values) {
      deck.push({ suit, value });
    }
  }

  // Add Jokers
  deck.push({ suit: 'Joker', color: 'Black' });
  deck.push({ suit: 'Joker', color: 'Red' });

  return deck;
}

function shuffle(array) {
  return array.sort(() => Math.random() - 0.5);
}

function startGame() {
  dungeon.innerHTML = ''; // Clear previous

  let deck = createDeck();

  // Remove Hearts 2–10 and Red Joker
  const faceUp = [];
  for (let value of ['2','3','4','5','6','7','8','9','10']) {
    faceUp.push({ suit: '♥', value });
  }
  faceUp.push({ suit: 'Joker', color: 'Red' });

  const faceUpKeys = new Set(faceUp.map(c => `${c.suit}-${c.value || c.color}`));
  const shuffled = deck.filter(c => !faceUpKeys.has(`${c.suit}-${c.value || c.color}`));

  const shuffledDeck = shuffle(shuffled);

  // Render cards
  for (let i = 0; i < shuffledDeck.length; i++) {
    const card = document.createElement('div');
    card.className = 'card face-down';
    card.style.gridRow = '1';
    card.style.gridColumn = `${i + 1}`;
    card.textContent = '🂠';
    dungeon.appendChild(card);
  }

  for (let i = 0; i < faceUp.length; i++) {
    const c = faceUp[i];
    const card = document.createElement('div');
    card.className = 'card';
    card.style.gridRow = '2';
    card.style.gridColumn = `${i + 1}`;
    card.textContent = c.suit === 'Joker' ? '🃏' : `${c.value}${c.suit}`;
    dungeon.appendChild(card);
  }
}

startBtn.addEventListener('click', startGame);
