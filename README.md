# Micro-Blogging API
With the creation of new competitors in the micro-blogging space, our team has decided to pivot from a platform designed to reasearch user behavior. We've changed our vision to capitilize on a smaller and wealthier demographic. We've done this by creating Birb: a luxury subscription-only micro-blogging social media designed to give wealthy people their own echo chamber for a substantial monthly fee. Capitalism has broken our spirits and now we crave profit and a desire to exploit our exploiters. The database has three tables divided into customers, posts, and subscriptions.

# API Endpoints
https://localhost:7241/api/Customer

https://localhost:7241/api/Customer/{id}

https://localhost:7241/api/Post

https://localhost:7241/api/Post/{id}

https://localhost:7241/api/Subscription

https://localhost:7241/api/Subscription/{id}

# HTTP Methods
GET api/Customer
- Returns all customers from the customer table

GET api/Customer/{id}
- Return a specific customer from the customer table

GET api/Post
- Returns all posts from the post table

GET api/Post/{id}
- Returns a specific post from the post table

GET api/Subscription
- Returns all subscriptions from the subscription table

GET api/Subscription/{id}
- Returns a specific subscription from the subscription table

POST api/Customer
- Creates a customer record in the customer table

POST api/Post
- Creates a post record in the post table

POST api/Subscription
- Creates a subscription record in the subscription table

DELETE api/Customer/{id}
- Deletes a specific customer record in the customer table

DELETE api/Post/{id}
- Deletes a specific post record in the post table

DELETE api/Subscription/{id}
- Deletes a specific subscription record in the subscription table


# Sample Request Body

{
    "postId": 2,
    "postText": "I got 10 followers",
    "customerName": "Hank"
}

# Sample Response Body

{
    "statusCode": 200,
    "statusDescription": "Woo! Customers found.",
    "customers": [
        {
            "customerId": 1,
            "subscriptionId": 3,
            "customerName": "Jack"
        },
        {
            "customerId": 2,
            "subscriptionId": 1,
            "customerName": "Elon"
        }
    ],
    "subscriptions": [],
    "posts": []
}


