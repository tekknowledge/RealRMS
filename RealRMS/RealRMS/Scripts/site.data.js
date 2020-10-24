var site = site || {};
(function (site) {
    site.data = {};
		
		/**********
		 * Orders *
		 **********/
		site.data.myOrders = [
			{
				id: "<a href='mock_order.htm?id=1'>1</a>",
				table: 1,
				server: "Sonic",
				inprogress: true,
				done: false,
				delivered: false	
			},
			{
				id: "<a href='mock_order.htm?id=2'>2</a>",
				table: 2,
				server: "Scrooge",
				inprogress: false,
				done: false,
				delivered: false	
			},
			{
				id: "<a href='mock_order.htm?id=3'>3</a>",
				table: 3,
				server: "Scrooge",
				inprogress: false,
				done: false,
				delivered: false	
			},
			{
				id: "<a href='mock_order.htm?id=3'>4</a>",
				table: 4,
				server: "Scrooge",
				inprogress: true,
				done: true,
				delivered: true
			}
		];

		site.data.transactions = [
		    {
		        id: '<a href="mock_trans.htm?idOrder=1">1</a>',
                table: 1
		    }
        ];
		site.data.orderDetails = [
			[
				{
					id: 1,
					seatNumber: 2
				},
				{
					id: 21,
					seatNumber: 3
				},
				{
					id: 4,
					seatNumber: 1
				}
			]
		];
		
		/*************
		 * Employees *
		 *************/
		site.data.employees = [
			{
				id: 1,
				name: "Sonic the Hedgehog",
				ssn: "111-11-1111",
				dob: '1/1/1900',
				street: '7800 York Rd',
				city: 'Towson',
				state: 'MD',
				zip: 21286,
				rate: "$19.75",
				roles: ['Restaurant Manager', 'Server', 'Runner', 'Cook']
			},
			{
				id: 2,
				name: "Scrooge McDuck",
				ssn: "123-45-6789",
				dob: "1/1/2000",
				street: "4501 N. Charles St",
				city: "Baltimore",
				state: "MD",
				zip: 21201,
				rate: "$8.30",
				roles: ["Server"]
			},
			{
				id: 3,
				name: "Darkwing Duck",
				ssn: "234-56-7890",
				dob: "1/1/1980",
				street: "1 E. Baltimore Street",
				city: "Baltimore",
				state: "MD",
				zip: 21201,
				rate: "$7.25",
				roles: ["Runner"]
			},
			{
				id: 4,
				name: "Huey Freeman",
				ssn: "555-55-5555",
				dob: "1/1/1990",
				street: "4501 Woodcrest Rd",
				city: "Columbia",
				state: "MD",
				zip: 21086,
				rate: "$9.50",
				roles: ["Cook"]
			}
		];
		
		/*************
		 * Inventory *
		 *************/
		site.data.inventory = [
			{
				id: 1,
				name: "Ground Beef",
				category: "Meat",
				onhand: '17',
				unit: 'Lbs',
				cost: '$3.99',
				reorder: false
			},
			{
				id: 2,
				name: "Ground Turkey",
				category: "Meat",
				onhand: '4',
				unit: 'Lbs',
				cost: '$2.99',
				reorder: true
			},
			{
				id: 3,
				name: "Chicken Breast",
				category: "Meat",
				onhand: '11',
				unit: 'Lbs',
				cost: '$1.99',
				reorder: true
			},
			{
				id: 4,
				name: "Cheddar Cheese",
				category: "Dairy",
				onhand: '1',
				unit: 'Lbs',
				cost: '$1.99',
				reorder: true
			},
			{
				id: 5,
				name: "Tomatoes",
				category: "Produce",
				onhand: '14',
				unit: 'Units',
				cost: '$1.00',
				reorder: false
			},
			{
				id: 6,
				name: "Potatoes",
				category: "Grains/Starches",
				onhand: '23',
				unit: 'Lbs',
				cost: '$.39',
				reorder: false
			}
		];
	
		site.data.inventoryList = [
				{
					id: 1,
					name: "<a href='mock_inventory.htm?id=1'>Ground Beef</a>",
					category: "Meat",
					onhand: '17',
					unit: 'Lbs',
					cost: '$3.99',
					reorder: false,
					actions: "<a id='deleteIcon1' href='javascript:inventoryList.deleteRow(1);'>Delete</a>"
				},
				{
					id: 2,
					name: "<a href='mock_inventory.htm?id=2'>Ground Turkey</a>",
					category: "Meat",
					onhand: '4',
					unit: 'Lbs',
					cost: '$2.99',
					reorder: true,
					actions: "<a id='deleteIcon2' href='javascript:inventoryList.deleteRow(2);'>Delete</a>"
				},
				{
					id: 3,
					name: "<a href='mock_inventory.htm?id=3'>Chicken Breast</a>",
					category: "Meat",
					onhand: '11',
					unit: 'Lbs',
					cost: '$1.99',
					reorder: true,
					actions: "<a id='deleteIcon3' href='javascript:inventoryList.deleteRow(3);'>Delete</a>"
				},
				{
					id: 4,
					name: "<a href='mock_inventory.htm?id=4'>Cheddar Cheese</a>",
					category: "Dairy",
					onhand: '1',
					unit: 'Lbs',
					cost: '$1.99',
					reorder: true,
					actions: "<a id='deleteIcon4' href='javascript:inventoryList.deleteRow(4);'>Delete</a>"
				},
				{
					id: 5,
					name: "<a href='mock_inventory.htm?id=5'>Tomatoes</a>",
					category: "Produce",
					onhand: '14',
					unit: 'Units',
					cost: '$1.00',
					reorder: false,
					actions: "<a id='deleteIcon5' href='javascript:inventoryList.deleteRow(5);'>Delete</a>"
				},
				{
					id: 6,
					name: "<a href='mock_inventory.htm?id=6'>Potatoes</a>",
					category: "Grains/Starches",
					onhand: '23',
					unit: 'Lbs',
					cost: '$.39',
					reorder: false,
					actions: "<a id='deleteIcon6' href='javascript:inventoryList.deleteRow(6);'>Delete</a>"
				}
		];
		
		/********
		 * Menu *
		 ********/
		site.data.menu = [
			{
				id: 1,
				name: "Buffalo Wings",
				category: "Appetizers",
				description:'Deep fried chicken wings coated in a spicy hot sauce',
				cost: '$8.99',
				actions: "<a id='deleteIcon1' href='javascript:menuList.deleteRow(1);'>Delete</a>"
			},
			{
				id: 2,
				name: "Mozzarella Sticks",
				category: "Appetizers",
				description:'Deep fried mozzarella served with homemade marinara sauce.' ,
				cost: '$6.99',
				actions: "<a id='deleteIcon2' href='javascript:menuList.deleteRow(2);'>Delete</a>"

			},
			{
				id: 10,
				name: "Onion Rings",
				category: "Appetizers",
				description:'Deep fried onions served with a zesty sauce.' ,
				cost: '$5.99',
				actions: "<a id='deleteIcon3' href='javascript:menuList.deleteRow(3);'>Delete</a>"
			},
			{
				id: 11,
				name: "Taquitos",
				category: "Appetizers",
				description:'Small rolled up chicken and pork stuffed tacos served with a chipotle sauce' ,
				cost: '$5.99',
				actions: "<a id='deleteIcon4' href='javascript:menuList.deleteRow(4);'>Delete</a>"
			},
			{
				id: 3,
				name: "Chicken Parm",
				category: "Entrees",
				description: 'Breaded chicken breast, deep fried, served over a bed of spaghetti and marinara sauce.',
				cost: '$16.99',
				actions: "<a id='deleteIcon5' href='javascript:menuList.deleteRow(5);'>Delete</a>"
			},
			{
				id: 12,
				name: "Pot Pie",
				category: "Entrees",
				description: 'Flaky pie crust with turkey, gravy, and vegetables baked inside.',
				cost: '$12.99',
				actions: "<a id='deleteIcon6' href='javascript:menuList.deleteRow(6);'>Delete</a>"
			},
			{
				id: 13,
				name: "Grilled Chicken",
				category: "Entrees",
				description: 'Lemon pepper seasoned grilled chicken breast served with steamed brocolli and rice.',
				cost: '$12.99',
				actions: "<a id='deleteIcon7' href='javascript:menuList.deleteRow(7);'>Delete</a>"
			},
			{
				id: 14,
				name: "Tip siroloin",
				category: "Entrees",
				description: 'Seasoned steak served with creamy mashed potatoes and sauteed cabbage. Served medium well, ask if you prefer something else.',
				cost: '$18.99',
				actions: "<a id='deleteIcon8' href='javascript:menuList.deleteRow(8);'>Delete</a>"
			},
			{
				id: 4,
				name: "Cheese Steak",
				category: "Subs",
				description:'Thinly sliced grilled sirloin steak with choice of cheese.',
				cost: '$7.99',
				actions: "<a id='deleteIcon9' href='javascript:menuList.deleteRow(9);'>Delete</a>"
			},
			{
				id: 5,
				name: "Italian Coldcut",
				category: "Subs",
				description:'Sliced salami, capacolla, and priscuitto on a toasted sub roll dressed with oil and vinegar.',
				cost: '$7.99',
				actions: "<a id='deleteIcon10' href='javascript:menuList.deleteRow(10);'>Delete</a>"
			},
			{
				id: 15,
				name: "Hamburger",
				category: "Subs",
				description:'Ground beef cooked hot and juicy on a wheat kaiser roll. Served with french fries.',
				cost: '$10.99',
				actions: "<a id='deleteIcon11' href='javascript:menuList.deleteRow(11);'>Delete</a>"
			},
			{
				id: 16,
				name: "Cheeseburger",
				category: "Subs",
				description:'Ground beef with american cheese cooked hot and juicy on a wheat kaiser roll. Served with french fries.',
				cost: '$10.99',
				actions: "<a id='deleteIcon12' href='javascript:menuList.deleteRow(12);'>Delete</a>"
			},
			{
				id: 6,
				name: "Canoli",
				category: "Deserts",
				description:'Sweet Ricotta cream filling in a fried hallow dough shell.',
				cost: '$4.99',
				actions: "<a id='deleteIcon13' href='javascript:menuList.deleteRow(13);'>Delete</a>"
			},
			{
				id: 17,
				name: "Fudge Brownie",
				category: "Deserts",
				description:'Double chocolate brownie served with a scoop of vanilla ice cream.',
				cost: '$4.99',
				actions: "<a id='deleteIcon14' href='javascript:menuList.deleteRow(14);'>Delete</a>"
			},
			{
				id: 18,
				name: "Garden Salad",
				category: "Salads",
				description:'Romain, cucumbers, grape tomato, red onion, served with italian dressing.',
				cost: '$6.99',
				actions: "<a id='deleteIcon15' href='javascript:menuList.deleteRow(15);'>Delete</a>"
			},
			{
				id: 19,
				name: "Chicken Garden Salad",
				category: "Salads",
				description:'Grilled chicken, Romaine, cucumbers, grape tomato, red onion, served with honey mustard dressing.',
				cost: '$10.99',
				actions: "<a id='deleteIcon16' href='javascript:menuList.deleteRow(16);'>Delete</a>"
			},
			{
				id: 20,
				name: "Beef Chili",
				category: "Soups",
				description:'A rich meaty chili that includes vegetables and pinto beans',
				cost: '$8.99',
				actions: "<a id='deleteIcon17' href='javascript:menuList.deleteRow(17);'>Delete</a>"
			},
			{
				id: 21,
				name: "Chicken noodle soup",
				category: "Soups",
				description:'Chicken breast chunks and egg noodles cooked in a tasty broth',
				cost: '$6.99',
				actions: "<a id='deleteIcon18' href='javascript:menuList.deleteRow(18);'>Delete</a>"
			},
			{
				id: 22,
				name: "Coke",
				category: "Beverages",
				description:'Free Refills',
				cost: '$1.99',
				actions: "<a id='deleteIcon19' href='javascript:menuList.deleteRow(19);'>Delete</a>"
			},
			{
				id: 23,
				name: "Diet Coke",
				category: "Beverages",
				description:'Free Refills',
				cost: '$1.99',
				actions: "<a id='deleteIcon20' href='javascript:menuList.deleteRow(20);'>Delete</a>"
			},
			{
				id: 24,
				name: "Sprite",
				category: "Beverages",
				description:'Free Refills',
				cost: '$1.99',
				actions: "<a id='deleteIcon21' href='javascript:menuList.deleteRow(21);'>Delete</a>"
			},
			{
				id: 25,
				name: "Root Beer",
				category: "Beverages",
				description:'Free Refills',
				cost: '$1.99',
				actions: "<a id='deleteIcon22' href='javascript:menuList.deleteRow(22);'>Delete</a>"
			},
			{
				id: 26,
				name: "Dr. Pepper",
				category: "Beverages",
				description:'Free Refills',
				cost: '$1.99',
				actions: "<a id='deleteIcon23' href='javascript:menuList.deleteRow(23);'>Delete</a>"
			},
			{
				id: 27,
				name: "Root Beer",
				category: "Beverages",
				description:'Free Refills',
				cost: '$1.99',
				actions: "<a id='deleteIcon24' href='javascript:menuList.deleteRow(24);'>Delete</a>"
			},
			{
				id: 28,
				name: "Coffee",
				category: "Beverages",
				description:'Free Refills',
				cost: '$1.99',
				actions: "<a id='deleteIcon25' href='javascript:menuList.deleteRow(25);'>Delete</a>"
			},
			{
				id: 29,
				name: "Green Tea",
				category: "Beverages",
				description:'Free Refills',
				cost: '$1.99',
				actions: "<a id='deleteIcon26' href='javascript:menuList.deleteRow(26);'>Delete</a>"
			},
			{
				id: 30,
				name: "Orange Juice",
				category: "Beverages",
				description:'No free refill. 8oz.',
				cost: '$2.99',
				actions: "<a id='deleteIcon27' href='javascript:menuList.deleteRow(27);'>Delete</a>"
			},
			{
				id: 31,
				name: "Screwdriver",
				category: "Alcoholic Beverages",
				description:'Vodka and house orage juice.',
				cost: '$7.99',
				actions: "<a id='deleteIcon28' href='javascript:menuList.deleteRow(28);'>Delete</a>"
			},
			{
				id: 32,
				name: "Long Island Iced Tea",
				category: "Alcoholic Beverages",
				description:'Not your standard iced tea.',
				cost: '$9.99',
				actions: "<a id='deleteIcon29' href='javascript:menuList.deleteRow(29);'>Delete</a>"
			}
		];
		
		site.data.menuList = [
			{
				id: 1,
				name: "<a href='mock_menu_item.html?id=1'>Buffalo Wings</a>",
				category: "Appetizers",
				description:'Deep fried chicken wings coated in a spicy hot sauce.',
				cost: '$8.99',
				actions: "<a id='deleteIcon1' href='javascript:menuList.deleteRow(1);'>Delete</a>"
			},
			{
				id: 2,
				name: "<a href='mock_menu_item.html?id=2'>Mozzarella Sticks </a>",
				category: "Appetizers",
				description:'Deep fried mozzarella served with homemade marinara sauce.',
				cost: '$6.99',
				actions: "<a id='deleteIcon2' href='javascript:menuList.deleteRow(2);'>Delete</a>"
			},
			{
			   id: 3,
				name: "<a href='mock_menu_item.html?id=3'>Chicken Parm </a>",
				category: "Entrees",
				description:'Breaded chicken breast, deep fried, served over a bed of spaghetti and marinara sauce.',
				cost: '$16.99',
				actions: "<a id='deleteIcon3' href='javascript:menuList.deleteRow(3);'>Delete</a>"
			},
			{
				id: 4,
				name: "<a href='mock_menu_item.html?id=4'>Cheese Steak </a>",
				category: "Subs",
				description:'Thinly sliced grilled sirloin steak with choice of cheese.',
				cost: '$7.99',
				actions: "<a id='deleteIcon4' href='javascript:menuList.deleteRow(4);'>Delete</a>"
			},
			{
				id: 5,
				name: "<a href='mock_menu_item.html?id=5'>Italian Coldcut  </a>",
				category: "Subs",
				description:'Sliced salami, capacolla, and priscuitto on a toasted sub roll dressed with oil and vinegar.',
				cost: '$7.99',
				actions: "<a id='deleteIcon5' href='javascript:menuList.deleteRow(5);'>Delete</a>"
			},
			{
				id: 6,
				name: "<a href='mock_menu_item.html?id=6'>Canoli </a>",
				category: "Deserts",
				description:'Sweet Ricotta cream filling in a fried hallow dough shell.',
				cost: '$4.99',
				actions: "<a id='deleteIcon6' href='javascript:menuList.deleteRow(6);'>Delete</a>"
			},
		];
}(site));