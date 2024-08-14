<template>
  <v-container>
    <v-row justify="space-between" align="center">
      <v-col>
        <v-toolbar flat>
          <v-toolbar-title>Products List</v-toolbar-title>
        </v-toolbar>
      </v-col>
      <v-col class="text-right">
        <v-btn color="primary" @click="navigateToCreateProduct">Create Product</v-btn>
      </v-col>
    </v-row>
    <v-row>
      <v-col>
        <v-data-table
          :headers="headers"
          :items="products"
          class="elevation-1"
          item-value="id"
          fixed-header
        >
          <template v-slot:[`item.actions`]="{ item }">
            <v-icon
              small
              @click="editProduct(item)"
              class="mr-2"
              color="blue"
            >
              mdi-pencil
            </v-icon>
            <v-icon
              small
              @click="viewDetails(item)"
              class="mr-2"
              color="green"
            >
              mdi-information
            </v-icon>
            <v-icon
              small
              @click="deleteProduct(item.id)"
              color="red"
            >
              mdi-delete
            </v-icon>
          </template>
        </v-data-table>
      </v-col>
    </v-row>
    <v-dialog v-model="dialog" max-width="500px">
      <v-card>
        <v-card-title>Product Details</v-card-title>
        <v-card-text>
          <p><strong>Product Name:</strong> {{ selectedProduct?.productName }}</p>
          <p><strong>Price:</strong> {{ selectedProduct?.price }}</p>
          <p><strong>Category:</strong> {{ selectedProduct?.category }}</p>
          <p><strong>Stored Quantity:</strong> {{ selectedProduct?.storedQuantity }}</p>
        </v-card-text>
        <v-card-actions>
          <v-btn color="primary" @click="dialog = false">Close</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-container>
</template>

<script>
import axios from 'axios';
import { useRouter } from 'vue-router';

export default {
  data() {
    return {
      headers: [
        { title: 'Product Name', value: 'productName' },
        { title: 'Price', value: 'price' },
        { title: 'Category', value: 'category' },
        { title: 'Stored Quantity', value: 'storedQuantity' },
        { title: 'Action', value: 'actions', sortable: false },
      ],
      products: [],
      dialog: false,
      selectedProduct: null,
    };
  },
  created() {
    this.fetchProducts();
  },
  methods: {
    editProduct(product) {
      this.$router.push({ path: `/edit-product/${product.id}` });
    },
    viewDetails(product) {
      this.selectedProduct = product;
      this.dialog = true;
    },
    async deleteProduct(id) {
      if (confirm('Are you sure you want to delete this product?')) {
        try {
          await axios.delete(`http://localhost:5099/api/Product/deleteProductById/${id}`);
          this.fetchProducts(); // Refresh the product list after deletion
        } catch (error) {
          console.error('Error deleting product:', error);
        }
      }
    },
    async fetchProducts() {
      try {
        const response = await axios.get('http://localhost:5099/api/Product/productList');
        if (response.status === 200) {
          this.products = response.data.data.products;
        } else {
          console.error('Failed to load products');
        }
      } catch (error) {
        console.error('Error fetching products:', error);
      }
    },
    navigateToCreateProduct() {
      this.$router.push('/create-product');
    },
  },
};
</script>

<style scoped>
.elevation-1 {
  margin-top: 20px;
}
.v-toolbar-title {
  font-weight: bold;
}
</style>
