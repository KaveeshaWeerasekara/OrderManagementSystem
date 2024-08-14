<template>
  <v-container>
    <v-form ref="form" v-model="valid" @submit.prevent="submitForm">
      <v-row>
        <!-- User Dropdown -->
        <v-col cols="12" md="6">
          <v-select
            v-model="order.userId"
            :item-props="itemProps"
            :items="users"
            item-text="name"
            item-value="id"
            :rules="[rules.required]"
            label="Select User"
            required
          ></v-select>
        </v-col>
      </v-row>
      <v-row>
            <!-- Product Dropdown -->
            <v-col cols="12" md="6">
          <v-select
            v-model="selectedProduct"
             :item-props="productProps"
            :items="products"
            item-text="productName"
            item-value="id"
            :rules="[rules.required]"
            label="Select Product"
            required
            
          ></v-select>
        </v-col>
        <v-col cols="12" md="4">
          <v-text-field
            v-model="quantity"
            :rules="[rules.required, rules.number,validateQuantity(selectedProduct)]"
            label="Quantity"
            type="number"
            required
          ></v-text-field>
        </v-col>
      
        <v-col cols="12" md="2">
          <v-btn color="primary" @click="addProduct">Add To Cart</v-btn>
        </v-col>
       </v-row>
     
       <v-row v-if="order.products.length">
        <v-col cols="12">
          <v-data-table :headers="tableHeaders" :items="order.products">
            <template v-slot:[`item.actions`]="{ item }">
              <v-btn icon small @click="removeProduct(item)">
                <v-icon>mdi-delete</v-icon>
              </v-btn>
            </template>
          </v-data-table>
        </v-col>
      </v-row>


      <v-row v-if="order.products.length">
        <v-col cols="12">
          <v-btn color="success" type="submit">Submit Order</v-btn>
        </v-col>
      </v-row>
    </v-form>
  </v-container>
</template>

<script>
import { ref, onMounted } from 'vue';
import axios from 'axios';

export default {
  data() {
    return {
      valid: false,
      order: {
        userId: null,
        products: [],
         quantity: 0,
      },
      selectedProduct: null,
      quantity: 1,
      users: [],
       products: [],
       tableHeaders: [
        { title: 'Product Name', value: 'productName' },
        { title: 'Quantity', value: 'quantity' },
        { title: 'Actions', value: 'actions', sortable: false }
      ],
      rules: {
        required: value => !!value || 'Required.',
        number: value => !isNaN(value) || 'Must be a number.',
      },
    };
  },
  methods: {
    
    async fetchUsersAndProducts() {
      try {
        const userResponse = await axios.get('http://localhost:5099/api/User/GetList');
        if (userResponse.status === 200) {
          this.users = userResponse.data.data.users.map(user => ({
            id: user.id,
            name: user.fName + " " + user.lName
          }));
        } 
        const productResponse = await axios.get('http://localhost:5099/api/Product/productList');
        console.log('Product response:', productResponse);
        if (productResponse.status === 200) {
          this.products = productResponse.data.data.products.map(product => ({
            id: product.id,
            productName: product.productName,
            quantity:product.storedQuantity
          }));
        }
      } catch (error) {
        console.error('Error fetching users:', error);
      }
    },
    itemProps (item) {
        return {
          title: item.name,
          subtitle: item.id,
        }
      },
    
    productProps (item) {
        return {
          title: item.productName,
          //subtitle: item.id,
        }
      },
    validateQuantity(value) {
      console.log(value);

      if (value = ! null) {

        var product = this.products.find(p => p.id == value);
        console.log(product);



       // return this.quantity <= product.stockQuantity || `Quantity must be less than or equal to ${product.stockQuantity}`;
      }


    },
      addProduct() {
        const product = this.products.find(p => p.id === this.selectedProduct);
  if (product && this.quantity > 0 && this.quantity <= product.stockQuantity) {
    this.order.products.push({
      productId: product.id,
      productName: product.productName,
      quantity: this.quantity
    });
    this.selectedProduct = null;
    this.quantity = 1;
  } else {
    console.error("Invalid quantity");
  }
    },
    removeProduct(item) {
      const index = this.order.products.indexOf(item);
      if (index !== -1) {
        this.order.products.splice(index, 1);
      }
    },
    async submitForm() {
      if (this.valid) {
        try {
          const response = await axios.post('http://localhost:5099/api/Order/createOrder', this.order);
          if (response.status === 200) {
            console.log('Order placed successfully');
            this.clearForm();
          } else {
            console.error('Failed to place order');
          }
        } catch (error) {
          console.error('Error placing order:', error);
        }
      }
    },
    clearForm() {
      this.order = {
        userId: null,
         productId: [],
        // quantity: 1,
      };
    }
  },
  mounted() {
    this.fetchUsersAndProducts();
    // this.fetchProducts();
  }
};
</script>

<style scoped>
/* Add any scoped styles here if needed */
</style>
