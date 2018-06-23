Products
========

The **Product** entity represents a single product that the documentation is written for. Each product can have multiple versions (releases). 

A document can only be linked to one product and the link is established through a product version. For more information, please refer to :doc:`versions`.

Data Model
^^^^^^^^^^

+--------------------+-----------------+---------------------------------------------+
| Property           | Type            | Description                                 |
+====================+=================+=============================================+
| id                 | integer         | Unique ID of the product.                   |
+--------------------+-----------------+---------------------------------------------+
| fullName           | string          | Marketing name of the product.              |
+--------------------+-----------------+---------------------------------------------+
| shortName          | string          | Short name of the product (up to 7          |
|                    |                 | characters long).                           |
+--------------------+-----------------+---------------------------------------------+
| alias              | string          | Alternative name of the product (e.g.       |
|                    |                 | internal name).                             |
+--------------------+-----------------+---------------------------------------------+

Creating Products
^^^^^^^^^^^^^^^^^^^

Create a Single Product
-----------------------

You can create a single product by executing a ``POST`` action against the ``api/v1/products`` endpoint.

.. code-block:: none

    POST api/v1/products


The body of the request requires a ``Product`` object. You can set the ``id`` property to ``0`` or exclude it from the object.

.. code-block:: javascript

    {
        "fullName": "Awesome Product",
        "shortName": "P1",
        "alias": "Product 1"
    }

If the product is created successfully, a ``201 Created`` code is returned. The response body contains the newly created product. 

.. code-block:: javascript

    {
        "id": 1,
        "fullName": "Awesome Product",
        "shortName": "P1",
        "alias": "Product 1"
    }

If the product cannot be saved, a ``400 Bad Request`` code is returned with a description of the error in the response body.

.. code-block:: javascript

    {
        "FullName": [
            "The FullName field is required."
        ],
        "ShortName": [
            "The Short name cannot be longer than 7 characters."
        ]
    }

Create Multiple Products
------------------------

If you are migrating to the *Documentation Repository* from another tool, it may be useful to create products in bulk. You can achieve this by executing a ``POST`` action against the ``api/v1/products/batch`` endpoint.

.. code-block:: none

    POST api/v1/products/batch

The body of the request accepts an array of ``Product`` objects. You can set the ``id`` property to ``0`` or exclude it entirely.

.. code-block:: javascript

    [
        {
            "fullName": "Awesome Product",
            "shortName": "P1",
            "alias": "Product 1"
        },
        {
            "fullName": "Nice Product",
            "shortName": "P2",
            "alias": "Product 2"
        }
    ]

If the products are created successfully, a ``201 Created`` code is returned. The response body contains an array of the newly created products. 

.. code-block:: javascript

    [
        {
            "id": 1,
            "fullName": "Awesome Product",
            "shortName": "P1",
            "alias": "Product 1"
        },
        {
            "id": 2,
            "fullName": "Nice Product",
            "shortName": "P2",
            "alias": "Product 2"
        }
    ]

If a product cannot be saved, a ``400 Bad Request`` code is returned with a description of the error in the response body.

.. code-block:: javascript

    {
        "[0].FullName": [
            "The FullName field is required."
        ],
        "[1].ShortName": [
            "The Short name cannot be longer than 7 characters."
        ]
    }


Retrieving Existing Products
^^^^^^^^^^^^^^^^^^^^^^^^^^^^

Retrieve All Products
---------------------

You can retrieve existing products by executing a ``GET`` action against the ``api/v1/products`` endpoint.

.. code-block:: none

    GET api/v1/products

The ``200 OK`` status code is returned. The body of the response contains an array of all the ``Product`` objects.

.. code-block:: javascript

    [
        {
            "id": 1,
            "fullName": "Awesome Product",
            "shortName": "P1",
            "alias": "Product 1"
        },
        {
            "id": 2,
            "fullName": "Nice Product",
            "shortName": "P2",
            "alias": "Product 2"
        },
        {
            "id": 3,
            "fullName": "Old Product",
            "shortName": "P3",
            "alias": "Product 3"
        }
    ]

If no products are found, a ``404 Not Found`` status code is returned.


Retrieve a Single Product
-------------------------

You can also retrieve a single product by executing a ``GET`` action against the ``api/v1/products/{id}`` endpoint where the ``{id}`` parameter refers to the ID of the product.

.. code-block:: none

    GET api/v1/products/1

The ``200 OK`` status code is returned. The body of the response contains a single ``Product`` object.

.. code-block:: javascript

    {
        "id": 1,
        "fullName": "Awesome Product",
        "shortName": "P1",
        "alias": "Product 1"
    }

If a product with a matching ID is cannot be found, a ``404 Not Found`` status code is returned.

.. _put-product:

Update a Product
^^^^^^^^^^^^^^^^^

You can modify an existing product by executing a ``PUT`` action against the ``api/v1/products/{id}`` endpoint where the ``{id}`` parameter refers to the ID of the product.

.. code-block:: none

    PUT api/v1/products/1

Use the request body to pass the updated ``Product`` object. Please note that you need to include all the properties of the object, including the ``ID``.

.. code-block:: javascript
    :emphasize-lines: 4

    {
        "id": 1,
        "fullName": "Awesome Product",
        "shortName": "AP",
        "alias": "Product 1"
    }

If the product is updated successfully, a ``204 No Content`` code is returned.

If the request was incorrect in any way, a ``400 Bad Request`` status code is returned, with the description of the error in the response body.

.. code-block:: javascript

    {
        "Invalid Product ID": [
            "The Product ID supplied in the query and the body of the request do not match."
        ]
    }

If a product with a matching ID is cannot be found, a ``404 Not Found`` status code is returned.

Remove a Product
^^^^^^^^^^^^^^^^^

In some cases, you may want to delete a product from the database. You can achieve this by executing a ``DELETE`` action against the ``api/v1/products/{id}`` endpoint where the ``{id}`` parameter refers to the ID of the ``Product`` object.

.. warning:: Due to a `bug <https://github.com/mihailo-stevanovic/documentation-repository/issues/3>`_, removing a product currently also removes all the related documents.

.. code-block:: none

    DELETE api/v1/products/1

The ``200 OK`` status code is returned. The body of the response contains the deleted ``Product`` object.

.. code-block:: javascript

    {
        "id": 1,
        "fullName": "Awesome Product",
        "shortName": "AP",
        "alias": "Product 1"
    }

If a product with a matching ID cannot be found, a ``404 Not Found`` status code is returned.