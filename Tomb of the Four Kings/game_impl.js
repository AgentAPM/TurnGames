

function makeDeck(){
	const deck = [];
	for(const suit of ["spades","clubs","diamonds","hearts"]){
		for(let rank=2;rank<=10;rank++){
			deck.push({
				rank:rank,suit:suit,
				is_number: true,
			})
		}
		for(const rank of ["J","Q","K","A"]){
			deck.push({
				rank:rank,suit:suit,
				is_number: false,
			})
		}
	}
	deck.push({
		rank:"joker",suit:"",
		is_number: false,
	})
		
	return deck;
}

function shuffle(arr, perm){
	if(perm && perm.length === arr.length){
		for (let i = arr.length - 1; i > 0; i--) {
			const j = perm[i];
			[arr[i], arr[j]] = [arr[j], arr[i]];
		}
	} else {
		for (let i = arr.length - 1; i > 0; i--) {
			const j = Math.floor(Math.random() * (i + 1));
			[arr[i], arr[j]] = [arr[j], arr[i]];
		}
	}
	return arr
}

class TombOfFourKings extends StateMachine {

	constructor() {
		super();
	}
	
	setup(){
		this.deck = makeDeck().filter(c => !(c.suit==="hearts" && c.is_number) );
		shuffle(this.deck);
		
		this.hp = 9;
		this.treasure = [];
		this.tombs = [];
		this.scrolls = 0;
		this.jacks_of_spades = 0;
		this.jacks_of_clubs = 0;
		this.jacks_of_diamonds = 0;
		this.jacks_of_hearts = 0;
		
		this.dungeon = [];
		this.phase = "delve";
		
		this.nextRoom();
		
		this.msg("You enter the dungeon.")
		this.decision([
			{label:"draw a card", run:this.drawCard}
		]);
	}
	
	tellStatus(){
		this.emit({
			type:"msg",
			text:`${this.hp}HP, [${this.deck.lenght} cards remaining]`,
		})
	}
	
	drawCard(){
		const card = this.deck.pop();
		if(card.is_number){
			switch(card.suit){
				case "spades"://monster
					
				break;
				case "diamonds"://chest
					
				break;
				case "clubs"://door
					
				break;
			}
		} else {
			switch(card.rank){
				case "J":
					switch(card.suit){
						case "spades":
							this.jacks_of_spades+=1;
							this.msg(``);
						break;
						case "clubs":
							this.jacks_of_clubs+=1;
							this.msg(``);
						break;
						case "diamonds":
							this.jacks_of_diamonds+=1;
							this.msg(``);
						break;
						case "hearts":
							this.jacks_of_hearts+=1;
							this.msg(``);
						break;
					}
				break;
				case "Q":
					
				break;
				case "K":
					this.msg("This room contains a tomb hoard!")
					this.treasure.push(card);
				break;
				case "A":
					if(this.scrolls>0){
						
					} else {
						
					}
				break;
				case "joker":
					this.msg("There's a scroll of light in this room.")
					this.treasure.push(card);
				break;
			}
			
		}
	}
	
	nextRoom(){
		this.challenge = null;
		this.action = null;
		this.treasure = [];
	}
	
	tackleRoom(){
		this.decision([
			{label:"draw a card", run:()=>{}},
			{label:"go berzerk!", run:()=>{}, omit: !(this.challenge && this.challenge.is_number && this.challenge.suit==="spades") },
			{label:"drop treasure", run:()=>{}, omit: !(this.challenge && this.challenge.is_number && this.challenge.suit==="spades") },
			{label:"pick the lock", run:()=>{}, omit: !(this.challenge && this.challenge.is_number && this.challenge.suit==="spades") },
			{label:"disarm the mechanism", run:()=>{}, omit: !(this.challenge && this.challenge.is_number && this.challenge.suit==="spades") },
		])
	}
}