import { createRouter, createWebHistory } from 'vue-router';
import Home from '../views/Home.vue';
import Users from '../views/Users.vue';
import Products from '../views/Products.vue';
import Orders from '../views/Orders.vue';
import UserForm from '../components/UserForm.vue';
import UpdateUsers from '../components/UpdateUsers.vue';
import ProductForm from '../components/ProductForm.vue';
import UpdateProduct from '../components/UpdateProduct.vue';


const routes = [
  { path: '/', component: Home },
  { path: '/users', component: Users },
  { path: '/create-user', component: UserForm },
  { path: '/products', component: Products },
  { path: '/orders', component: Orders },
  { path: '/edit-user/:id', component: UpdateUsers },
  { path: '/create-product', component: ProductForm },
  { path: '/edit-product/:id', component: UpdateProduct },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
